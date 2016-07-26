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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    partial class MainPage
    {

        private App viewModel;

        private DateTime lastTimeOfWrite;

        private IBandClient bandClient;

        class DataChunk
        {
            public double AcceleartionX { get; }
            public double AcceleartionY { get; }
            public double AcceleartionZ { get; }
            public double AngularVelocityX { get; }
            public double AngularVelocityY { get; }
            public double AngularVelocityZ { get; }
            public long Ticks { get; }

            public DataChunk(double acceleartionX,
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
                return String.Format("{0},{1},{2},{3},{4},{5},{6}",
                    this.AcceleartionX,this.AcceleartionY,this.AcceleartionZ,
                    this.AngularVelocityX,this.AngularVelocityY,this.AngularVelocityZ,
                    this.Ticks);
            }
        }

        List<DataChunk> dataChunks = new List<DataChunk>();

        // process data funciton
        async void ProcessData()
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
            dataChunks.Clear();
            await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);

        }
        async private void GyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Read data
                var reading = e.SensorReading;

                // Change Text Box
                this.gyroscopeTextBox.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
                this.gyroscopeTextBox.Text += " " + String.Format("{0,5:0.00}", reading.AccelerationY);
                this.gyroscopeTextBox.Text += " " + String.Format("{0,5:0.00}", reading.AccelerationZ);
                this.gyroscopeTextBox.Text += " " + String.Format("{0,5:0.00}", reading.AngularVelocityX);
                this.gyroscopeTextBox.Text += " " + String.Format("{0,5:0.00}", reading.AngularVelocityY);
                this.gyroscopeTextBox.Text += " " + String.Format("{0,5:0.00}", reading.AngularVelocityZ);

                // Add data to the array
                if (recordCheckBox.IsChecked != null && recordCheckBox.IsChecked.Value)
                {
                    this.dataChunks.Add(new DataChunk(reading.AccelerationX,
                        reading.AccelerationY,
                        reading.AccelerationZ,
                        reading.AngularVelocityX,
                        reading.AngularVelocityY,
                        reading.AngularVelocityZ,
                        DateTime.Now.Ticks));
                }
                if (reading.AccelerationX > 2)
                {
                    if ((DateTime.Now - lastTimeOfWrite).Seconds > 2)
                    {
                        // TODO: Make this work
                        // ControlPowerPoint(0);
                        ProcessData();
                        lastTimeOfWrite = DateTime.Now;
                    }
                }
            });
        }

        private void recordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //ProcessData();
        }

        [DllImport("PowerPointControllerAddin.dll")]
        public static extern void GetPowerPoint();

        [DllImport("PowerPointControllerAddin.dll")]
        public static extern void ControlPowerPoint(int actionIndex);

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

                await bandClient.SensorManager.Gyroscope.StartReadingsAsync();

            }
            catch (Exception ex)
            {
                this.viewModel.StatusMessage = ex.ToString();
            }
        }
    }

}
