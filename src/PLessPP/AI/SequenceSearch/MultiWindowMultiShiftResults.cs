/// <summary>
/// MultiWindowMultiShiftResults.cs
/// </summary>

namespace PLessPP.AI
{
    using System;
    using System.Collections.Generic;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftResults
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
        /// <param name="windowIndex"></param>
        /// <param name="distance"></param>
        public void AddDistanceToWindow(int windowIndex, double distance)
        {
            this.results[windowIndex].Add(distance);
        }
    }
}
