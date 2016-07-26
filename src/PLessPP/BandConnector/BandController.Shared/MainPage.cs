/*
    Copyright (c) Microsoft Corporation All rights reserved.  
 
    MIT License: 
 
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
    documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
    the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
    and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
 
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
 
    THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
    THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Microsoft.Band;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Core;
using Microsoft.Band.Notifications;
using Microsoft.Band.Sensors;

namespace BandController
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Networking;
    using Windows.Networking.Sockets;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private const int SecondsOfData = 4;
        private const int ItemsPerSecond = 64;
        private const int ItemsInChunk = SecondsOfData * ItemsPerSecond;

        private App viewModel;

        private DateTime lastTimeOfWrite;

        private IBandClient bandClient;

        private StreamSocket client;

        public class DataPoint
        {
            private double AcceleartionX { get; }
            private double AcceleartionY { get; }
            private double AcceleartionZ { get; }
            private double AngularVelocityX { get; }
            private double AngularVelocityY { get; }
            private double AngularVelocityZ { get; }
            public long Ticks { get; }

            public DataPoint(double acceleartionX,
                double acceleartionY,
                double acceleartionZ,
                double angularVelocityX,
                double angularVelocityY,
                double angularVelocityZ,
                long ticks)
            {
                this.AcceleartionX = acceleartionX;
                this.AcceleartionY = acceleartionY;
                this.AcceleartionZ = acceleartionZ;
                this.AngularVelocityX = angularVelocityX;
                this.AngularVelocityY = angularVelocityY;
                this.AngularVelocityZ = angularVelocityZ;
                this.Ticks = ticks;
            }

            public override String ToString()
            {
                return
                    $"{this.AcceleartionX},{this.AcceleartionY},{this.AcceleartionZ},{this.AngularVelocityX},{this.AngularVelocityY},{this.AngularVelocityZ},{this.Ticks}";
            }
        }

        private readonly ConcurrentQueue<DataPoint> dataPoints = new ConcurrentQueue<DataPoint>();

        public DataPoint[] GetCurrentChunk()
        {
            lock (this.dataPoints)
            {
                return this.dataPoints.ToArray();
            }
        }

        /// <summary>
        ///     TODO get from Andrea
        /// </summary>
        /// <param name="points"></param>
        private async void ProcessData(DataPoint[] points)
        {
            // This commented code is for when you want to write sample data to a file
            /*
            StorageFile outputFile =
                (await
                    folder.CreateFileAsync(
                        @"Sampledata_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".csv",
                        CreationCollisionOption.ReplaceExisting));
            var lines = new List<string>();
            for (var index = 0; index < dataChunks.Count; index++)
            {
                lines.Add(dataChunks[index].ToString());
            }
            await FileIO.AppendLinesAsync(outputFile, lines);
            dataChunks.Clear();
            */

            double averageAccX = 0;
            foreach (DataPoint point in points)
            {
                averageAccX += point.Ticks;
            }
            averageAccX /= points.Length;
            averageAccX /= 64848;

            if ( (Math.Ceiling(averageAccX) % 1000 == 2) && ((DateTime.Now - this.lastTimeOfWrite).Seconds > 1) )
            {
                // todo remove this
                await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
            }
        }

        private async void GyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            // Read data
            var reading = e.SensorReading;

            // Add data to the array
            lock (this.dataPoints)
            {
                var datapoint = new DataPoint(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ, reading.AngularVelocityX,
                    reading.AngularVelocityY, reading.AngularVelocityZ, reading.Timestamp.Ticks);
                // Add new data
                this.dataPoints.Enqueue(datapoint);

                // Throw out old data
                if(this.dataPoints.Count > ItemsInChunk)
                {
                    DataPoint toChug;
                    this.dataPoints.TryDequeue(out toChug);
                }

                // send data to win32 land
                try
                {
                    var streamWriter = new StreamWriter(this.client.OutputStream.AsStreamForWrite());
                    streamWriter.WriteLine(datapoint);
                    streamWriter.Flush();
                }
                catch
                {
                    // todo: probably the stream died we should let the user know
                }
            }

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Change Text Box
                this.gyroscopeTextBox.Text = $"{reading.AccelerationX,5:0.00}";
                this.gyroscopeTextBox.Text += $" {reading.AccelerationY,5:0.00}";
                this.gyroscopeTextBox.Text += $" {reading.AccelerationZ,5:0.00}";
                this.gyroscopeTextBox.Text += $" {reading.AngularVelocityX,5:0.00}";
                this.gyroscopeTextBox.Text += $" {reading.AngularVelocityY,5:0.00}";
                this.gyroscopeTextBox.Text += $" {reading.AngularVelocityZ,5:0.00}";
            });
        }
        
        private async void RunProgram()
        {
            // connect to server
            this.client = new StreamSocket();
            var hostname = new HostName("127.0.0.1");
            await client.ConnectAsync(hostname, "7039");
            this.viewModel.StatusMessage = "Running ...";
            
            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    this.viewModel.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

                int samplesReceived = 0; // the number of Gyroscope samples received
                // Subscribe to Gyroscope data.
                bandClient.SensorManager.Gyroscope.ReadingChanged += this.GyroscopeReadingChanged;
                
                /*Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        DataPoint[] pointsClone = this.GetCurrentChunk();
                        this.ProcessData(pointsClone);
                    }
                });*/

                Task task2 = Task.Run(async () =>
                {
                    var streamReader = new StreamReader(this.client.InputStream.AsStreamForRead());
                    while (true)
                    {
                        await streamReader.ReadLineAsync();
                        await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
                    }
                });

                await bandClient.SensorManager.Gyroscope.StartReadingsAsync();

            }
            catch (Exception ex)
            {
                this.viewModel.StatusMessage = ex.ToString();
            }
        }
    }

}
