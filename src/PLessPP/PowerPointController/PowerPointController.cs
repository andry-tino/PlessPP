/// <summary>
/// PowerPointController.cs
/// </summary>

namespace PowerPointController
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;
    using System.Threading.Tasks;

    using MSPP = Microsoft.Office.Interop.PowerPoint;

    using PLessPP.AI;
    using PLessPP.Data;
    using PLessPP.Similarity;

    /// <summary>
    /// Controller for the presentation.
    /// </summary>
    /// <remarks>
    /// Current implementation uses sockets: to change into WCF.
    /// </remarks>
    public class PowerPointController
    {
        /// <summary>
        /// Get the running PowerPoint instance on this machine
        /// </summary>
        /// <returns></returns>
        public static MSPP.Application GetPowerPoint()
        {
            MSPP.Application instance;
            try
            {
                instance = (MSPP.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application");
                Console.WriteLine("Found a running instance of PowerPoint, attached.");
            }
            catch (Exception)
            {
                Console.WriteLine("No running PowerPoint instance found, starting new one.");
                instance = new MSPP.Application();
                instance.Activate();
            }
            return instance;
        }

        /// <summary>
        /// Control the PowerPoint presentation and make it skip a slide when a file changes
        /// </summary>
        /// <param name="powerPoint"></param>
        public static void ControlPowerPoint(MSPP.Application powerPoint)
        {
            Console.WriteLine("Controlling the PowerPoint Presentation");

            // Set up socket
            var endpoint = new IPEndPoint(IPAddress.Loopback, 7039);

            while (true)
            {
                try
                {
                    var server = new TcpListener(endpoint);
                    server.Start();

                    var connection = server.AcceptTcpClient(); // blocks

                    var reader = new StreamReader(connection.GetStream());
                    var writer = new StreamWriter(connection.GetStream());
                    
                    SimpleNormalizer normalizer = new SimpleNormalizer();
                    Point[] p = { new Point(1, 2, 3, 4, 5, 6, 7) };
                    Sequence goldenBaseline = new Sequence(normalizer, p);
                    int[] windowSize = { goldenBaseline.Length };

                    var searchAlgorithm = new MultiWindowMultiShiftSearch(
                        1, 
                        windowSize, 
                        goldenBaseline, 
                        new DynamicTimeWarpingAlgorithm(new AbsoluteDifferencePointDistanceCalculator()), 
                        true);

                    var searchDecider = new MultiWindowMultiShiftThresholdSearchDecider(3);
                    var chunkBuffer = new SimpleChunkBuffer();
                    

                    var processor = new ChunkConsumer(searchAlgorithm, searchDecider, chunkBuffer, normalizer);
                    
                    // Configure callback
                    processor.OnGesturePerformed += () =>
                    {
                        MSPP.SlideShowWindows slideShowWindows = powerPoint.SlideShowWindows;
                        if (slideShowWindows.Count < 1)
                        {
                            return;
                        }

                        MSPP.SlideShowWindow slideShowWindow = slideShowWindows[1];
                        slideShowWindow.View.Next();

                        // notify band to vibrate
                        writer.WriteLine("buzz");
                        writer.Flush();

                        Console.WriteLine("We have a gesture!");
                    };

                    // Start chunk processing worker task
                    Task task = Task.Run(() =>
                    {
                        processor.Run();
                    });

                    // use this thread to fill the buffer
                    while (connection.Connected)
                    {
                        var command = reader.ReadLine();
                        //Console.WriteLine($"Received command {command}");
                        
                        // now to """deserialize""" the data
                        var data = command.Split(',');
                        try
                        {
                            Point point = new Point(
                                Double.Parse(data[0]),
                                Double.Parse(data[1]),
                                Double.Parse(data[2]),
                                Double.Parse(data[3]),
                                Double.Parse(data[4]),
                                Double.Parse(data[5]),
                                Int64.Parse(data[6])
                            );

                            chunkBuffer.EnqueueData(point);
                        }
                        catch
                        {
                            // invalid data - discard
                            Console.WriteLine("Received badly formatted data");
                            continue;
                        }
                    }

                    server.Stop();
                }
                catch
                {
                }
            }
        }


        private static void Main(string[] args)
        {
            var powerPoint = GetPowerPoint();
            ControlPowerPoint(powerPoint);
            powerPoint.Quit();
        }
    }
}