/// <summary>
/// BaselineProvider.cs
/// </summary>

namespace PLessPP.AI
{
    using System;
    using System.IO;

    using PLessPP.Data;
    using PLessPP.Data.Connectors;

    /// <summary>
    /// Provides the baseline.
    /// </summary>
    public class BaselineProvider
    {
        private const string sampleDataAndreaFileName = "SampleData_Andrea_Positive_001.csv";
        private const string sampleDataConstantinFileName = "SampleData_Constantin_Positive_001.csv";

        // Cached values
        private Sequence baseline;

        /// <summary>
        /// Gets the baseline.
        /// </summary>
        public Sequence Baseline
        {
            get
            {
                if (this.baseline == null)
                {
                    CSVDataConnector dataConnector = new CSVDataConnector(GetPath(SelectedBaseline));
                    this.baseline = dataConnector.Data;
                }
                
                return this.baseline;
            }
        }

        private string SelectedBaseline
        {
            get { return sampleDataConstantinFileName; }
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "BaselineSequences", fileName);
        }
    }
}
