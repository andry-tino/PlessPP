/**
 * MainPage.cs
 * 
 * PLessPP - Copyright (C) 2016
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace PLessPP.Device.BandController
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Networking;
    using Windows.Networking.Sockets;
    using Windows.UI.Core;

    using Microsoft.Band;
    using Microsoft.Band.Notifications;
    using Microsoft.Band.Sensors;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private App viewModel;

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

        private async void GyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            // Read data
            var reading = e.SensorReading;

            // Add data to the array
            lock (this.dataPoints)
            {
                var datapoint = new DataPoint(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ, reading.AngularVelocityX,
                    reading.AngularVelocityY, reading.AngularVelocityZ, reading.Timestamp.Ticks);
                
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

                // Subscribe to Gyroscope data.
                bandClient.SensorManager.Gyroscope.ReadingChanged += this.GyroscopeReadingChanged;

                Task task = Task.Run(async () =>
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
