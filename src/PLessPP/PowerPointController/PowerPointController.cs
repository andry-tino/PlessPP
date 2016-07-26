// --------------------------------------------------------------------------
// <copyright file="PowerPointController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
namespace PowerPointController
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class PowerPointController
    {
        /// <summary>
        /// Get the running PowerPoint instance on this machine
        /// </summary>
        /// <returns></returns>
        public static Microsoft.Office.Interop.PowerPoint.Application GetPowerPoint()
        {
            Microsoft.Office.Interop.PowerPoint.Application instance;
            try
            {
                instance =
                    (Microsoft.Office.Interop.PowerPoint.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application");
                Console.WriteLine("Found a running instance of PowerPoint, attached.");
            }
            catch (Exception)
            {
                Console.WriteLine("No running PowerPoint instance found, starting new one.");
                instance = new Microsoft.Office.Interop.PowerPoint.Application();
                instance.Activate();
            }
            return instance;
        }

        /// <summary>
        /// Control the PowerPoint presentation and make it skip a slide when a file changes
        /// </summary>
        /// <param name="powerPoint"></param>
        public static void ControlPowerPoint(Microsoft.Office.Interop.PowerPoint.Application powerPoint)
        {
            Console.WriteLine("Controlling the PowerPoint Presentation");

            // Set up socket
            var endpoint = new IPEndPoint(IPAddress.Loopback, 7039);
            double previousValue = 0;

            while (true)
            {
                try
                {
                    var server = new TcpListener(endpoint);
                    server.Start();
                    var connection = server.AcceptTcpClient();
                    var reader = new StreamReader(connection.GetStream());

                    while (connection.Connected)
                    {
                        var command = reader.ReadLine();
                        //Console.WriteLine($"Received command {command}");
                        
                        // now to """deserialize""" the data
                        var data = command.Split(',');

                        double value = Double.Parse(data[0]);
                        if (value > 2 && previousValue < 2)
                        {
                            Microsoft.Office.Interop.PowerPoint.SlideShowWindows slideShowWindows =
                                    powerPoint.SlideShowWindows;
                            if (slideShowWindows.Count < 1) continue;
                            Microsoft.Office.Interop.PowerPoint.SlideShowWindow slideShowWindow = slideShowWindows[1];
                            slideShowWindow.View.Next();
                        }

                        previousValue = value;
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
            Microsoft.Office.Interop.PowerPoint.Application powerPoint = GetPowerPoint();
            ControlPowerPoint(powerPoint);
            powerPoint.Quit();
        }
    }
}