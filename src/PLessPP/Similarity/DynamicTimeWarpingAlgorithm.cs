/// <summary>
/// 
/// </summary>

namespace PLessPP.Similarity
{
    using System;
    using System.Linq;

    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public class DynamicTimeWarpingAlgorithm : ISimilarityAlgorithm
    {
        private const double Infinity = System.Double.PositiveInfinity;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTimeWarpingAlgorithm"/>.
        /// </summary>
        public DynamicTimeWarpingAlgorithm()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        /// <returns></returns>
        public double ComputeSimilarity(Sequence sequence1, Sequence sequence2)
        {
            double[] seq1 = sequence1.ToArray();
            double[] seq2 = sequence2.ToArray();

            return Compute(seq1, seq2);
        }

        private static double Compute(double[] sequence1, double[] sequence2)
        {
            int l1 = sequence1.Length;
            int l2 = sequence2.Length;

            double[,] dtw = new double[l1, l2];

            // Initialization
            for (int i = 0; i < l1; i++)
            {
                dtw[i, 0] = Infinity;
            }
            for (int i = 0; i < l2; i++)
            {
                dtw[0, i] = Infinity;
            }
            dtw[0, 0] = 0;

            // Processing
            for (int i = 0; i < l1; i++)
            {
                for (int j = 0; j < l2; j++)
                {
                    double distance = GetDistance(sequence1[i], sequence2[j]);

                    dtw[i, j] = distance + GetMinimum(
                        dtw[i, j + 1], 
                        dtw[i + 1, j], 
                        dtw[i, j]);
                }
            }

            return dtw[l1 - 1, l2 - 1];
        }

        private static double GetDistance(double x, double y)
        {
            return x >= y ? x - y : y - x;
        }

        private static double GetMinimum(double x, double y, double z)
        {
            return (new double[] { x, y, z }).Min();
        }
    }
}
