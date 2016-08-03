/**
 * ConfigureFileSystemDialog.xaml.cs
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
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Windows.Foundation;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Provider;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Dialog for filesystem configuration.
    /// 
    /// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409.
    /// </summary>
    public sealed partial class ConfigureFileSystemDialog : ContentDialog
    {
        public ConfigureFileSystemDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void PickerButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string filePath = await this.GetStoragePath();
            this.FilePathTextBox.Text = filePath ?? string.Empty;
        }

        private async Task<string> GetStoragePath()
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Gesture log file", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Log";

            // Getting the path
            StorageFile file = await savePicker.PickSaveFileAsync();

            return file?.Path;
        } 
    }
}
