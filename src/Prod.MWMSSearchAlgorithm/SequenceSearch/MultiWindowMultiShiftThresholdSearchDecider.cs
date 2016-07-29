/**
 * MultiWindowMultiShiftThresholdSearchDecider.cs
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftThresholdSearchDecider : ISearchDecider
    {
        private readonly double threshold;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threshold"></param>
        public MultiWindowMultiShiftThresholdSearchDecider(double threshold)
        {
            this.threshold = threshold;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MatchFound(object results)
        {
            var mwmsResults = (MultiWindowMultiShiftResults)results;
            bool matchFound;
            var allWindowsResults = new List<bool>();

            foreach (var windowDistances in mwmsResults)
            {
                allWindowsResults.Add(CompareWindowMatch(windowDistances));
            }

            // Deciding: match if all windows found a match
            matchFound = !allWindowsResults.Any(value => value == false);
            
            return matchFound;
        }

        private bool CompareWindowMatch(IEnumerable<double> windowDistances)
        {
            Console.WriteLine($"confidence: {windowDistances.Min()}");
            foreach (var distance in windowDistances)
            {
                if (distance <= this.threshold)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
