/**
 * MainPage.xaml.cs
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

    using Microsoft.Band;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// 
    /// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ConcurrentQueue<GyroscopeVector> vectors;

        private string filePath;

        private IBandClient bandClient;
        private CancellationTokenSource fileWriterCancellationTokenSource;
        private Task fileWriterTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            // Initializing data
            this.vectors = new ConcurrentQueue<GyroscopeVector>();

            // Ready to start the business logic
            this.RunProgram();
        }

        private string FilePath
        {
            get { return this.filePath; }

            set
            {
                this.filePath = value;
                this.OnFilePathChanged(this.filePath);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void FileLocationMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ConfigureFileSystemDialog dialog = new ConfigureFileSystemDialog();
            await dialog.ShowAsync();

            this.FilePath = dialog.FilePath;
        }
    }
}
