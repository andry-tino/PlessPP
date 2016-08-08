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
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    using Microsoft.Band;
    using Microsoft.Band.Notifications;
    using Microsoft.Band.Sensors;

    using PLessPP.Device.BandGestureFlowRetrieval;

    using PLessPP.Device.UIControls;

    /// <summary>
    /// Contains the business logic.
    /// 
    /// TODO: Apply proper resource handling on application close.
    /// </summary>
    public sealed partial class MainPage
    {
        private async void OnGyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            GyroscopeVector vector = new GyroscopeVector()
            {
                AccelerationX = e.SensorReading.AccelerationX,
                AccelerationY = e.SensorReading.AccelerationY,
                AccelerationZ = e.SensorReading.AccelerationZ,
                AngularVelocityX = e.SensorReading.AngularVelocityX,
                AngularVelocityY = e.SensorReading.AngularVelocityY,
                AngularVelocityZ = e.SensorReading.AngularVelocityZ,
            };

            this.vectors.Enqueue(vector);
        }

        private async void RunProgram()
        {
            try
            {
                IBandInfo[] pairedBands = null;

                for (int attempts = 3; attempts >= 0; attempts--)
                {
                    // Get the list of Microsoft Bands paired to the phone.
                    pairedBands = await BandClientManager.Instance.GetBandsAsync();

                    if (pairedBands.Length >= 1)
                    {
                        break;
                    }
                }

                if (pairedBands.Length < 1)
                {
                    await new MessageDialog("This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.").ShowAsync();
                    return;
                }

                // Connect to Microsoft Band.
                this.bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

                // Initialize visualizer (this will subscribe to gyroscope data)
                SporadicGyroscopeValuesVisualizer visualizer = new SporadicGyroscopeValuesVisualizer(
                    bandClient.SensorManager.Gyroscope, this.Dispatcher,
                    this.AccXTextBox, this.AccXMinor1TextBox, this.AccXMinor2TextBox, this.AccXMinor3TextBox,
                    this.AccYTextBox, this.AccYMinor1TextBox, this.AccYMinor2TextBox, this.AccYMinor3TextBox,
                    this.AccZTextBox, this.AccZMinor1TextBox, this.AccZMinor2TextBox, this.AccZMinor3TextBox,
                    this.RotXTextBox, this.RotXMinor1TextBox, this.RotXMinor2TextBox, this.RotXMinor3TextBox,
                    this.RotYTextBox, this.RotYMinor1TextBox, this.RotYMinor2TextBox, this.RotYMinor3TextBox,
                    this.RotZTextBox, this.RotZMinor1TextBox, this.RotZMinor2TextBox, this.RotZMinor3TextBox,
                    Color.FromArgb(255, 103, 33, 122), Color.FromArgb(0, 0, 0, 0));

                // Subscribe to Gyroscope data.
                this.bandClient.SensorManager.Gyroscope.ReadingChanged += this.OnGyroscopeReadingChanged;

                // Spawn file writer
                this.StartFileWriterTask();

                // Starting sensor reading
                await this.bandClient.SensorManager.Gyroscope.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog($"An error occurred: {ex.ToString()}").ShowAsync();
            }
        }

        private void StopFileWriterTask()
        {
            if (this.fileWriterCancellationTokenSource == null)
            {
                return;
            }

            this.fileWriterCancellationTokenSource.Cancel();
        }

        private void StartFileWriterTask()
        {
            this.fileWriterCancellationTokenSource = new CancellationTokenSource();
            this.fileWriterTask = new Task(this.WriteFile, this.fileWriterCancellationTokenSource.Token);
            this.fileWriterTask.Start();

        }

        private async void WriteFile()
        {
            for (;;)
            {
                await Task.Delay(1000);

                if (this.vectors.Count == 0)
                {
                    continue;
                }

                if (this.FilePath == null || this.FilePath == string.Empty)
                {
                    // Stop
                    //await new MessageDialog($"File path: {this.FilePath} is not valid. Choose a different one!").ShowAsync();
                    this.FailureTextBlock.Visibility = Visibility.Visible;
                    return;
                }
                
                File.AppendAllText(this.FilePath, "Hi there");
            }
        }

        private void OnFilePathChanged(string filePath)
        {
            this.StopFileWriterTask();

            this.FailureTextBlock.Visibility = Visibility.Collapsed;

            this.StartFileWriterTask();
        }
    }
}
