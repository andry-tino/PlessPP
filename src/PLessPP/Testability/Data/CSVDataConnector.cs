/// <summary>
/// CSVDataConnector.cs
/// </summary>

namespace PLessPP.Testing.Testability.Data
{
    using System;
    using System.IO;

    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public class CSVDataConnector : IDataConnector
    {
        private Sequence sequence;
        private string filePath;

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
        /// 
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
            string content = File.ReadAllText(this.filePath);

            // Split commans and get values
            string[] numbers = content.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            // Convert to double
            double[] values = new double[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                values[i] = double.Parse(numbers[i]);
            }

            this.sequence = new Sequence(values);
        }
    }
}
