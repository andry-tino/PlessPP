/**
 * BaselineProvider.cs
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

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;
    using System.IO;

    using PLessPP.Data;
    using PLessPP.Data.Connectors;

    /// <summary>
    /// Provides the baseline.
    /// 
    /// TODO: Make it a singleton
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
            get { return sampleDataAndreaFileName; }
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "BaselineSequences", fileName);
        }
    }
}
