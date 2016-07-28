/// <summary>
/// MultiWindowMultiShiftResults.cs
/// </summary>

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
