/// <summary>
/// MultiWindowMultiShiftResults.cs
/// </summary>

namespace PLessPP.AI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftResults : IEnumerable<double[]>
    {
        List<double>[] results;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowsCount"></param>
        public MultiWindowMultiShiftResults(int windowsCount)
        {
            this.results = new List<double>[windowsCount];
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
        public IEnumerator<double[]> GetEnumerator()
        {
            return this.results.GetEnumerator() as IEnumerator<double[]>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.results.GetEnumerator();
        }
    }
}
