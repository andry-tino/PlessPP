/// <summary>
/// CSVDataConnector.cs
/// </summary>

namespace PLessPP.Testing.Testability.Data
{
    using System;
    using System.IO;

    using PLessPP.Data;

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
