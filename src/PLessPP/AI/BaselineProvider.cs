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
        private const string sampleDataFileName = "SampleData_Andrea_Positive_001.csv";

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
                    CSVDataConnector dataConnector = new CSVDataConnector(GetPath(sampleDataFileName));
                    this.baseline = dataConnector.Data;
                }
                
                return this.baseline;
            }
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "BaselineSequences", fileName);
        }
    }
}
