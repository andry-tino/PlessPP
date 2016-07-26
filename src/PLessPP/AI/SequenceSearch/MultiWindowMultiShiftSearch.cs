/// <summary>
/// MultiWindowMultiShiftSearch.cs
/// </summary>

namespace PLessPP.AI
{
    using System;

    using PLessPP.Data;
    using PLessPP.Similarity;
    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftSearch : ISequenceSearcher
    {
        private readonly int shift; // delta
        private readonly int[] windowSizes;
        private readonly Sequence baseline;
        private readonly ISimilarityAlgorithm similarityAlgorithm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="windowSizes"></param>
        public MultiWindowMultiShiftSearch(int shift, int[] windowSizes, Sequence baseline, ISimilarityAlgorithm similarityAlgorithm)
        {
            // TODO: Add checks

            this.shift = shift;
            this.windowSizes = windowSizes;
            this.baseline = baseline;
            this.similarityAlgorithm = similarityAlgorithm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="results"></param>
        public void Search(Sequence sequence, out object results)
        {
            var mwmsResults = new MultiWindowMultiShiftResults(this.windowSizes.Length);

            for (int k = 0; k < this.windowSizes.Length; k++)
            {
                var windowSize = this.windowSizes[k];

                for (int i = 0; ; i++)
                {
                    int lastIndex = i * shift + windowSize;
                    if (lastIndex >= sequence.Length)
                    {
                        lastIndex = sequence.Length - 1;
                    }

                    Sequence windowedSequence = sequence[i * shift, lastIndex];

                    double similarity = this.similarityAlgorithm.ComputeSimilarity(this.baseline, windowedSequence);
                    mwmsResults.AddDistanceToWindow(k, similarity);
                }
            }

            results = mwmsResults;
        }
    }
}
