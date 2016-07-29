/**
 * DynamicTimeWarpingAlgorithm.cs
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

namespace PLessPP.AI.Similarity
{
    using System;
    using System.Linq;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public class DynamicTimeWarpingAlgorithm : ISimilarityAlgorithm
    {
        private const double Infinity = System.Double.PositiveInfinity;

        private IPointDistanceCalculator pointDistanceCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTimeWarpingAlgorithm"/>.
        /// </summary>
        public DynamicTimeWarpingAlgorithm(IPointDistanceCalculator pointDistanceCalculator)
        {
            if (pointDistanceCalculator == null)
            {
                throw new ArgumentNullException(nameof(pointDistanceCalculator));
            }

            this.pointDistanceCalculator = pointDistanceCalculator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        /// <returns></returns>
        public double ComputeSimilarity(Sequence sequence1, Sequence sequence2)
        {
            Point[] seq1 = sequence1.ToArray();
            Point[] seq2 = sequence2.ToArray();

            return this.Compute(seq1, seq2);
        }

        private double Compute(Point[] sequence1, Point[] sequence2)
        {
            int l1 = sequence1.Length;
            int l2 = sequence2.Length;

            double[,] dtw = new double[l1, l2];

            // Initialization
            for (int i = 1; i < l1; i++)
            {
                dtw[i, 0] = Infinity;
            }
            for (int i = 1; i < l2; i++)
            {
                dtw[0, i] = Infinity;
            }
            dtw[0, 0] = 0;

            // Processing
            for (int i = 1; i < l1; i++)
            {
                for (int j = 1; j < l2; j++)
                {
                    double distance = this.GetDistance(sequence1[i], sequence2[j]);

                    dtw[i, j] = distance + GetMinimum(
                        dtw[i - 1, j    ], 
                        dtw[i    , j - 1], 
                        dtw[i - 1, j - 1]);
                }
            }

            return dtw[l1 - 1, l2 - 1];
        }

        private double GetDistance(Point x, Point y)
        {
            return this.pointDistanceCalculator.GetDistance(x, y);
        }

        private static double GetMinimum(double x, double y, double z)
        {
            return (new double[] { x, y, z }).Min();
        }
    }
}
