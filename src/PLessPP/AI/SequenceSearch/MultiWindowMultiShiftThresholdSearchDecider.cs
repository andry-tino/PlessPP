/// <summary>
/// MultiWindowMultiShiftThresholdSearchDecider.cs
/// </summary>

namespace PLessPP.AI.SequenceSearch
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

        // Cached values
        private bool? matchFound;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
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

            if (!this.matchFound.HasValue)
            {
                var allWindowsResults = new List<bool>();

                foreach (var windowDistances in mwmsResults)
                {
                    allWindowsResults.Add(CompareWindowMatch(windowDistances));
                }

                // Deciding: match if all windows found a match
                this.matchFound = !allWindowsResults.Any(value => value == false);
            }

            return this.matchFound.Value;
        }

        private bool CompareWindowMatch(double[] windowDistances)
        {
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
