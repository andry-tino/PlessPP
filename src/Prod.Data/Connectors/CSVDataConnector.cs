/**
 * CSVDataConnector.cs
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

namespace PLessPP.Data.Connectors
{
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public class CSVDataConnector : IDataConnector
    {
        private const int numberOfValidNumberPerLine = 6;

        private Sequence sequence;
        private string filePath;

        private INormalizer normalizer = new SimpleNormalizer();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public CSVDataConnector(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException("Invalid file path.", nameof(path));
            }

            this.filePath = path;
        }

        /// <summary>
        /// Will aggregate all data for each timestamp. Timestamp is removed.
        /// </summary>
        public Sequence Data
        {
            get
            {
                if (this.sequence == null)
                {
                    this.GetData();
                }

                return this.sequence;
            }
        }

        private void GetData()
        {
            string[] lines = File.ReadAllLines(this.filePath);

            Point[] values = new Point[lines.Length];

            for (int i = 0; i < values.Length; i++)
            {
                // Split commans and get values
                string[] numbers = lines[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (numbers.Length != numberOfValidNumberPerLine + 1) // Timestamp will be discarded
                {
                    throw new InvalidDataException("Provided data not in correct format!");
                }

                values[i] = new Point()
                {
                    AccelerationX   = double.Parse(numbers[0]),
                    AccelerationY   = double.Parse(numbers[1]),
                    AccelerationZ   = double.Parse(numbers[2]),
                    GyroX           = double.Parse(numbers[3]),
                    GyroY           = double.Parse(numbers[4]),
                    GyroZ           = double.Parse(numbers[5]),
                    Timestamp       = long.Parse(numbers[6])
                };
            }

            this.sequence = new Sequence(this.normalizer, values);
        }
    }
}
