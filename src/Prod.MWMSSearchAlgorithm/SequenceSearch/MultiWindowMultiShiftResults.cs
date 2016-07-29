/**
 * MultiWindowMultiShiftResults.cs
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
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftResults : IEnumerable<IEnumerable<double>>
    {
        private List<double>[] results;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowsCount"></param>
        public MultiWindowMultiShiftResults(int windowsCount)
        {
            this.results = new List<double>[windowsCount];

            for (int i = 0; i < windowsCount; i++)
            {
                this.results[i] = new List<double>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return this.results.Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowIndex"></param>
        /// <param name="distance"></param>
        public void AddDistanceToWindow(int windowIndex, double distance)
        {
            this.results[windowIndex].Add(distance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
        {
            return ((IEnumerable<IEnumerable<double>>)this.results).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Outputs a string representation of the results.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            const string openBracket = "{";
            const string closeBracket = "}";
            const string separator = ",";

            string output = string.Empty;

            output += openBracket;

            foreach (var window in this.results)
            {
                output += openBracket;

                foreach (var value in window)
                {
                    output += value.ToString();
                    output += separator;
                }

                output += closeBracket;
                output += separator;
            }

            output += closeBracket;

            output = output.Replace(string.Format("{0}{1}", separator, closeBracket), closeBracket);

            return output;
        }
    }
}
