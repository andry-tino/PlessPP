/// <summary>
/// DTWStaticSequenceTestSuite.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.Similarity.Data;
    using PLessPP.Similarity;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class DTWStaticSequenceTestSuite
    {
        [TestMethod]
        public void EqualSmallSequences()
        {
            double[] values = new double[] { 1, 2, 3, 4 };
            Sequence sequence1 = new Sequence(values);
            Sequence sequence2 = new Sequence(values);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreEqual(0, similarity, "Same sequences must have similarity 0!");
        }

        [TestMethod]
        public void DifferentSmallSequences()
        {
            Sequence sequence1 = new Sequence(1, 2, 3, 4);
            Sequence sequence2 = new Sequence(5, 6, 7, 8);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Different sequences must not have similarity 0!");
        }

        [TestMethod]
        public void EqualLargeSequences()
        {
            double[] values = new double[1000];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = i;
            }

            Sequence sequence1 = new Sequence(values);
            Sequence sequence2 = new Sequence(values);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreEqual(0, similarity, "Same sequences must have similarity 0!");
        }

        [TestMethod]
        public void DifferentLargeSequences()
        {
            double[] values1 = new double[1000];
            double[] values2 = new double[1000];

            for (int i = 0; i < values1.Length; i++)
            {
                values1[i] = i;
            }
            for (int i = 0; i < values2.Length; i++)
            {
                values2[i] = i + values1.Length;
            }

            Sequence sequence1 = new Sequence(values1);
            Sequence sequence2 = new Sequence(values2);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Different sequences must not have similarity 0!");
        }
    }
}
