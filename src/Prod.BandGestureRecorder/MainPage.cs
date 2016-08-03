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

namespace PLessPP.Device.BandGestureRecorder
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    
    using Windows.UI.Core;
    using Windows.UI.Popups;
    using Windows.UI.Xaml.Media;

    using Microsoft.Band;
    using Microsoft.Band.Notifications;
    using Microsoft.Band.Sensors;

    using PLessPP.Device.BandGestureFlowRetrieval;

    /// <summary>
    /// Contains the business logic.
    /// </summary>
    public sealed partial class MainPage
    {
        private App viewModel;
        private IBandClient bandClient;

        private async void GyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            // Read data
            var reading = e.SensorReading;

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                string a1 = $"{reading.AccelerationX,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string a2 = $"{reading.AccelerationY,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string a3 = $"{reading.AccelerationZ,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g1 = $"{reading.AngularVelocityX,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g2 = $"{reading.AngularVelocityY,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g3 = $"{reading.AngularVelocityZ,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);

                double threshold = 0.6d;

                if (new Random().NextDouble() > threshold)
                {
                    this.AccXTextBox.Text = a1.Substring(0, Math.Min(2, a1.Length)).Trim();
                    this.AccXMinor3TextBox.Text = this.AccXMinor2TextBox.Text;
                    this.AccXMinor2TextBox.Text = this.AccXMinor1TextBox.Text;
                    this.AccXMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.AccXTextBox.Foreground = reading.AccelerationX < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }

                if (new Random().NextDouble() > threshold)
                {
                    this.AccYTextBox.Text = a2.Substring(0, Math.Min(2, a2.Length)).Trim();
                    this.AccYMinor3TextBox.Text = this.AccYMinor2TextBox.Text;
                    this.AccYMinor2TextBox.Text = this.AccYMinor1TextBox.Text;
                    this.AccYMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.AccYTextBox.Foreground = reading.AccelerationY < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }

                if (new Random().NextDouble() > threshold)
                {
                    this.AccZTextBox.Text = a3.Substring(0, Math.Min(2, a3.Length)).Trim();
                    this.AccZMinor3TextBox.Text = this.AccZMinor2TextBox.Text;
                    this.AccZMinor2TextBox.Text = this.AccZMinor1TextBox.Text;
                    this.AccZMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.AccZTextBox.Foreground = reading.AccelerationZ < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }

                if (new Random().NextDouble() > threshold)
                {
                    this.RotXTextBox.Text = g1.Substring(0, Math.Min(2, g1.Length)).Trim();
                    this.RotXMinor3TextBox.Text = this.RotXMinor2TextBox.Text;
                    this.RotXMinor2TextBox.Text = this.RotXMinor1TextBox.Text;
                    this.RotXMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.RotXTextBox.Foreground = reading.AngularVelocityX < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }

                if (new Random().NextDouble() > threshold)
                {
                    this.RotYTextBox.Text = g2.Substring(0, Math.Min(2, g2.Length)).Trim();
                    this.RotYMinor3TextBox.Text = this.RotYMinor2TextBox.Text;
                    this.RotYMinor2TextBox.Text = this.RotYMinor1TextBox.Text;
                    this.RotYMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.RotYTextBox.Foreground = reading.AngularVelocityY < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }

                if (new Random().NextDouble() > threshold)
                {
                    this.RotZTextBox.Text = g3.Substring(0, Math.Min(2, g3.Length)).Trim();
                    this.RotZMinor3TextBox.Text = this.RotZMinor2TextBox.Text;
                    this.RotZMinor2TextBox.Text = this.RotZMinor1TextBox.Text;
                    this.RotZMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.RotZTextBox.Foreground = reading.AngularVelocityZ < 0
                        ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 103, 33, 122))
                        : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                }
            });
        }

        private async void RunProgram()
        {
            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    await new MessageDialog("This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.").ShowAsync();
                    return;
                }

                // Connect to Microsoft Band.
                bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

                // Subscribe to Gyroscope data.
                bandClient.SensorManager.Gyroscope.ReadingChanged += this.GyroscopeReadingChanged;

                await bandClient.SensorManager.Gyroscope.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog($"An error occurred: {ex.ToString()}").ShowAsync();
            }
        }
    }
}
