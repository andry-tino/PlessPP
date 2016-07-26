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
using System.Runtime.InteropServices;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Microsoft.Band.Notifications;
using Microsoft.Band.Sensors;

namespace BandController
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    partial class MainPage
    {
        private const int SecondsOfData = 4;
        private const int ItemsPerSecond = 64;
        private const int ItemsInChunk = SecondsOfData * ItemsPerSecond;

        private App viewModel;

        private DateTime lastTimeOfWrite;

        private IBandClient bandClient;

        class DataPoint
        {
            public double AcceleartionX { get; }
            public double AcceleartionY { get; }
            public double AcceleartionZ { get; }
            public double AngularVelocityX { get; }
            public double AngularVelocityY { get; }
            public double AngularVelocityZ { get; }
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

        // process data funciton
        async void ProcessData(DataPoint[] points)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var now = DateTime.Now;
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

            StorageFile outputFile =
                (await
                    folder.GetFileAsync(@"Sampledata.txt"));
            var lines = new List<string>();
            lines.Add("New entry " + now);
            await FileIO.AppendLinesAsync(outputFile, lines);
            await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
        }

        private async void GyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            // Read data
            var reading = e.SensorReading;

            // Add data to the array
            lock (this.dataPoints)
            {
                // Add new data
                this.dataPoints.Enqueue(new DataPoint(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ, reading.AngularVelocityX,
                    reading.AngularVelocityY, reading.AngularVelocityZ, reading.Timestamp.Ticks));

                // Throw out old data
                if(this.dataPoints.Count > ItemsInChunk)
                {
                    DataPoint toChug;
                    this.dataPoints.TryDequeue(out toChug);
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
            lastTimeOfWrite = DateTime.Now;
            this.viewModel.StatusMessage = "Running ...";

            // TODO: Make this work
            // GetPowerPoint();

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

                int[] demoData = {0, 1, 2, 3, 4, 3, 4, 5, 6, 7, 8, 4, 3};

                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        DataPoint[] pointsClone;
                        lock (this.dataPoints)
                        {
                            pointsClone = this.dataPoints.ToArray();
                        }
                        this.ProcessData(pointsClone);
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
