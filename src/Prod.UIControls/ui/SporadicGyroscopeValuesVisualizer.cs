/**
 * SporadicGyroscopeValuesVisualizer.cs
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

namespace PLessPP.Device.UIControls
{
    using System;

    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    using Microsoft.Band;
    using Microsoft.Band.Sensors;

    /// <summary>
    /// 
    /// </summary>
    public class SporadicGyroscopeValuesVisualizer : IDisposable
    {
        private const double Threshold = 0.6d;

        private readonly SolidColorBrush positiveColor;
        private readonly SolidColorBrush negativeColor;

        private readonly IBandSensor<IBandGyroscopeReading> gyroscopeReader;
        private readonly CoreDispatcher dispatcher;

        private readonly TextBox accXMainTextBox, accXMinor1TextBox, accXMinor2TextBox, accXMinor3TextBox;
        private readonly TextBox accYMainTextBox, accYMinor1TextBox, accYMinor2TextBox, accYMinor3TextBox;
        private readonly TextBox accZMainTextBox, accZMinor1TextBox, accZMinor2TextBox, accZMinor3TextBox;
        private readonly TextBox rotXMainTextBox, rotXMinor1TextBox, rotXMinor2TextBox, rotXMinor3TextBox;
        private readonly TextBox rotYMainTextBox, rotYMinor1TextBox, rotYMinor2TextBox, rotYMinor3TextBox;
        private readonly TextBox rotZMainTextBox, rotZMinor1TextBox, rotZMinor2TextBox, rotZMinor3TextBox;

        private EventHandler<BandSensorReadingEventArgs<IBandGyroscopeReading>> gyroscopeReadingChangedEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SporadicGyroscopeValuesVisualizer"/> class.
        /// </summary>
        /// <param name="gyroscopeReader"></param>
        /// <param name="dispatcher"></param>
        /// <param name="accXMainTextBox"></param>
        /// <param name="accXMinor1TextBox"></param>
        /// <param name="accXMinor2TextBox"></param>
        /// <param name="accXMinor3TextBox"></param>
        /// <param name="accYMainTextBox"></param>
        /// <param name="accYMinor1TextBox"></param>
        /// <param name="accYMinor2TextBox"></param>
        /// <param name="accYMinor3TextBox"></param>
        /// <param name="accZMainTextBox"></param>
        /// <param name="accZMinor1TextBox"></param>
        /// <param name="accZMinor2TextBox"></param>
        /// <param name="accZMinor3TextBox"></param>
        /// <param name="rotXMainTextBox"></param>
        /// <param name="rotXMinor1TextBox"></param>
        /// <param name="rotXMinor2TextBox"></param>
        /// <param name="rotXMinor3TextBox"></param>
        /// <param name="rotYMainTextBox"></param>
        /// <param name="rotYMinor1TextBox"></param>
        /// <param name="rotYMinor2TextBox"></param>
        /// <param name="rotYMinor3TextBox"></param>
        /// <param name="rotZMainTextBox"></param>
        /// <param name="rotZMinor1TextBox"></param>
        /// <param name="rotZMinor2TextBox"></param>
        /// <param name="rotZMinor3TextBox"></param>
        public SporadicGyroscopeValuesVisualizer(
            IBandSensor<IBandGyroscopeReading> gyroscopeReader, CoreDispatcher dispatcher,
            TextBox accXMainTextBox, TextBox accXMinor1TextBox, TextBox accXMinor2TextBox, TextBox accXMinor3TextBox,
            TextBox accYMainTextBox, TextBox accYMinor1TextBox, TextBox accYMinor2TextBox, TextBox accYMinor3TextBox,
            TextBox accZMainTextBox, TextBox accZMinor1TextBox, TextBox accZMinor2TextBox, TextBox accZMinor3TextBox,
            TextBox rotXMainTextBox, TextBox rotXMinor1TextBox, TextBox rotXMinor2TextBox, TextBox rotXMinor3TextBox,
            TextBox rotYMainTextBox, TextBox rotYMinor1TextBox, TextBox rotYMinor2TextBox, TextBox rotYMinor3TextBox,
            TextBox rotZMainTextBox, TextBox rotZMinor1TextBox, TextBox rotZMinor2TextBox, TextBox rotZMinor3TextBox,
            Color positiveColor, Color negativeColor)
        {
            if (gyroscopeReader == null) throw new ArgumentNullException(nameof(gyroscopeReader));
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));

            if (accXMainTextBox == null) throw new ArgumentNullException(nameof(accXMainTextBox));
            if (accXMinor1TextBox == null) throw new ArgumentNullException(nameof(accXMinor1TextBox));
            if (accXMinor2TextBox == null) throw new ArgumentNullException(nameof(accXMinor2TextBox));
            if (accXMinor3TextBox == null) throw new ArgumentNullException(nameof(accXMinor3TextBox));

            if (accYMainTextBox == null) throw new ArgumentNullException(nameof(accYMainTextBox));
            if (accYMinor1TextBox == null) throw new ArgumentNullException(nameof(accYMinor1TextBox));
            if (accYMinor2TextBox == null) throw new ArgumentNullException(nameof(accYMinor2TextBox));
            if (accYMinor3TextBox == null) throw new ArgumentNullException(nameof(accYMinor3TextBox));

            if (accZMainTextBox == null) throw new ArgumentNullException(nameof(accZMainTextBox));
            if (accZMinor1TextBox == null) throw new ArgumentNullException(nameof(accZMinor1TextBox));
            if (accZMinor2TextBox == null) throw new ArgumentNullException(nameof(accZMinor2TextBox));
            if (accZMinor3TextBox == null) throw new ArgumentNullException(nameof(accZMinor3TextBox));

            if (rotXMainTextBox == null) throw new ArgumentNullException(nameof(rotXMainTextBox));
            if (rotXMinor1TextBox == null) throw new ArgumentNullException(nameof(rotXMinor1TextBox));
            if (rotXMinor2TextBox == null) throw new ArgumentNullException(nameof(rotXMinor2TextBox));
            if (rotXMinor3TextBox == null) throw new ArgumentNullException(nameof(rotXMinor3TextBox));

            if (rotYMainTextBox == null) throw new ArgumentNullException(nameof(rotYMainTextBox));
            if (rotYMinor1TextBox == null) throw new ArgumentNullException(nameof(rotYMinor1TextBox));
            if (rotYMinor2TextBox == null) throw new ArgumentNullException(nameof(rotYMinor2TextBox));
            if (rotYMinor3TextBox == null) throw new ArgumentNullException(nameof(rotYMinor3TextBox));

            if (rotZMainTextBox == null) throw new ArgumentNullException(nameof(rotZMainTextBox));
            if (rotZMinor1TextBox == null) throw new ArgumentNullException(nameof(rotZMinor1TextBox));
            if (rotZMinor2TextBox == null) throw new ArgumentNullException(nameof(rotZMinor2TextBox));
            if (rotZMinor3TextBox == null) throw new ArgumentNullException(nameof(rotZMinor3TextBox));

            if (positiveColor == null) throw new ArgumentNullException(nameof(positiveColor));
            if (negativeColor == null) throw new ArgumentNullException(nameof(negativeColor));

            this.gyroscopeReader = gyroscopeReader;

            this.accXMainTextBox = accXMainTextBox;
            this.accXMinor1TextBox = accXMinor1TextBox;
            this.accXMinor2TextBox = accXMinor2TextBox;
            this.accXMinor3TextBox = accXMinor3TextBox;

            this.accYMainTextBox = accYMainTextBox;
            this.accYMinor1TextBox = accYMinor1TextBox;
            this.accYMinor2TextBox = accYMinor2TextBox;
            this.accYMinor3TextBox = accYMinor3TextBox;

            this.accZMainTextBox = accZMainTextBox;
            this.accZMinor1TextBox = accZMinor1TextBox;
            this.accZMinor2TextBox = accZMinor2TextBox;
            this.accZMinor3TextBox = accZMinor3TextBox;

            this.rotXMainTextBox = rotXMainTextBox;
            this.rotXMinor1TextBox = rotXMinor1TextBox;
            this.rotXMinor2TextBox = rotXMinor2TextBox;
            this.rotXMinor3TextBox = rotXMinor3TextBox;

            this.rotYMainTextBox = rotYMainTextBox;
            this.rotYMinor1TextBox = rotYMinor1TextBox;
            this.rotYMinor2TextBox = rotYMinor2TextBox;
            this.rotYMinor3TextBox = rotYMinor3TextBox;

            this.rotZMainTextBox = rotZMainTextBox;
            this.rotZMinor1TextBox = rotZMinor1TextBox;
            this.rotZMinor2TextBox = rotZMinor2TextBox;
            this.rotZMinor3TextBox = rotZMinor3TextBox;

            this.positiveColor = new SolidColorBrush(positiveColor);
            this.negativeColor = new SolidColorBrush(negativeColor);
        }

        private void Initialize()
        {
            this.AttachEvents();
        }

        private void AttachEvents()
        {
            this.gyroscopeReadingChangedEventHandler = this.OnGyroscopeReadingChanged;
            this.gyroscopeReader.ReadingChanged += this.gyroscopeReadingChangedEventHandler;
        }

        private void DetachEvents()
        {
            this.gyroscopeReader.ReadingChanged -= this.gyroscopeReadingChangedEventHandler;
            this.gyroscopeReadingChangedEventHandler = null;
        }

        private async void OnGyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            // Read data
            var reading = e.SensorReading;

            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                string a1 = $"{reading.AccelerationX,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string a2 = $"{reading.AccelerationY,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string a3 = $"{reading.AccelerationZ,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g1 = $"{reading.AngularVelocityX,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g2 = $"{reading.AngularVelocityY,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);
                string g3 = $"{reading.AngularVelocityZ,5:0.00}".Replace(".", string.Empty).Replace("-", string.Empty);

                if (new Random().NextDouble() > Threshold)
                {
                    this.accXMainTextBox.Text = a1.Substring(0, Math.Min(2, a1.Length)).Trim();
                    this.accXMinor3TextBox.Text = this.accXMinor2TextBox.Text;
                    this.accXMinor2TextBox.Text = this.accXMinor1TextBox.Text;
                    this.accXMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.accXMainTextBox.Foreground = reading.AccelerationX < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }

                if (new Random().NextDouble() > Threshold)
                {
                    this.accYMainTextBox.Text = a2.Substring(0, Math.Min(2, a2.Length)).Trim();
                    this.accYMinor3TextBox.Text = this.accYMinor2TextBox.Text;
                    this.accYMinor2TextBox.Text = this.accYMinor1TextBox.Text;
                    this.accYMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.accYMainTextBox.Foreground = reading.AccelerationY < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }

                if (new Random().NextDouble() > Threshold)
                {
                    this.accZMainTextBox.Text = a3.Substring(0, Math.Min(2, a3.Length)).Trim();
                    this.accZMinor3TextBox.Text = this.accZMinor2TextBox.Text;
                    this.accZMinor2TextBox.Text = this.accZMinor1TextBox.Text;
                    this.accZMinor1TextBox.Text = a1.Substring(0, Math.Min(4, a1.Length)).Trim();
                    this.accZMainTextBox.Foreground = reading.AccelerationZ < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }

                if (new Random().NextDouble() > Threshold)
                {
                    this.rotXMainTextBox.Text = g1.Substring(0, Math.Min(2, g1.Length)).Trim();
                    this.rotXMinor3TextBox.Text = this.rotXMinor2TextBox.Text;
                    this.rotXMinor2TextBox.Text = this.rotXMinor1TextBox.Text;
                    this.rotXMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.rotXMainTextBox.Foreground = reading.AngularVelocityX < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }

                if (new Random().NextDouble() > Threshold)
                {
                    this.rotYMainTextBox.Text = g2.Substring(0, Math.Min(2, g2.Length)).Trim();
                    this.rotYMinor3TextBox.Text = this.rotYMinor2TextBox.Text;
                    this.rotYMinor2TextBox.Text = this.rotYMinor1TextBox.Text;
                    this.rotYMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.rotYMainTextBox.Foreground = reading.AngularVelocityY < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }

                if (new Random().NextDouble() > Threshold)
                {
                    this.rotZMainTextBox.Text = g3.Substring(0, Math.Min(2, g3.Length)).Trim();
                    this.rotZMinor3TextBox.Text = this.rotZMinor2TextBox.Text;
                    this.rotZMinor2TextBox.Text = this.rotZMinor1TextBox.Text;
                    this.rotZMinor1TextBox.Text = g1.Substring(0, Math.Min(4, g1.Length)).Trim();
                    this.rotZMainTextBox.Foreground = reading.AngularVelocityZ < 0
                        ? this.negativeColor
                        : this.positiveColor;
                }
            });
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            this.DetachEvents();
        }
    }
}
