/// <summary>
/// MultiWindowMultiShiftThresholdSearchDecider.cs
/// </summary>

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
