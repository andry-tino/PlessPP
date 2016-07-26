/// <summary>
/// MultiWindowMultiShiftSearch.cs
/// </summary>

namespace PLessPP.AI
{
    using System;

    using PLessPP.Data;
    using Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftSearch : ISequenceSearcher
    {
        private readonly int shift;
        private readonly int[] windowSizes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="windowSizes"></param>
        public MultiWindowMultiShiftSearch(int shift, int[] windowSizes)
        {
            // TODO: Add checks

            this.shift = shift;
            this.windowSizes = windowSizes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="results"></param>
        public void Search(Sequence sequence, out object results)
        {
            throw new NotImplementedException();
        }
    }
}
