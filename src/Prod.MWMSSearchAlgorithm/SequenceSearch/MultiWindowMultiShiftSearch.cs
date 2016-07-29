/**
 * MultiWindowMultiShiftSearch.cs
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

    using PLessPP.Data;
    using PLessPP.AI.Similarity;

    /// <summary>
    /// 
    /// </summary>
    public class MultiWindowMultiShiftSearch : ISequenceSearcher
    {
        private readonly int shift; // delta
        private readonly int[] windowSizes;
        private readonly Sequence baseline;
        private readonly ISimilarityAlgorithm similarityAlgorithm;
        private readonly bool normalize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="windowSizes"></param>
        /// <param name="baseline"></param>
        /// <param name="similarityAlgorithm"></param>
        /// <param name="normalize"></param>
        public MultiWindowMultiShiftSearch(int shift, int[] windowSizes, Sequence baseline, 
            ISimilarityAlgorithm similarityAlgorithm, bool normalize = false)
        {
            // TODO: Add checks

            this.shift = shift;
            this.windowSizes = windowSizes;
            this.similarityAlgorithm = similarityAlgorithm;

            this.baseline = normalize ? baseline.Normalize() : baseline;
            this.normalize = normalize;
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

                // Attention: here we might lose a residual window which does not have the expected size but lower, 
                // we discard that data at the moment
                for (int i = 0; i <= sequence.Length - windowSize; i += shift)
                {
                    int lastIndex = i + windowSize - 1;

                    Sequence windowedSequence = sequence[i, lastIndex];
                    if (this.normalize)
                    {
                        windowedSequence = windowedSequence.Normalize();
                    }

                    double similarity = this.similarityAlgorithm.ComputeSimilarity(this.baseline, windowedSequence);

                    // Normalizing the similarity by the sum of the two sequences' lengths
                    // Attention: this normalization is not the same as normalizing the sequences as we act, here, on the y axis
                    double maxSequenceLength = this.baseline.Length + windowedSequence.Length - 1; // TODO: WTF is -1 needed for?
                    similarity = similarity / maxSequenceLength;

                    mwmsResults.AddDistanceToWindow(k, similarity);
                }
            }

            results = mwmsResults;
        }
    }
}
