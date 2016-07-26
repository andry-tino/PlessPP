/// <summary>
/// DTWStaticSequenceTestSuite.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.Data;
    using PLessPP.Similarity;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class DTWStaticSequenceTestSuite
    {
        private INormalizer normalizer = new SimpleNormalizer();

        [TestMethod]
        public void EqualSmallSequences()
        {
            Point[] values = new Point[] {
                Utils.BuildPoint(1),
                Utils.BuildPoint(2),
                Utils.BuildPoint(3),
                Utils.BuildPoint(4) };

            Sequence sequence1 = new Sequence(normalizer, values);
            Sequence sequence2 = new Sequence(normalizer, values);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreEqual(0, similarity, "Same sequences must have similarity 0!");
        }

        [TestMethod]
        public void DifferentSmallSequences()
        {
            Sequence sequence1 = new Sequence(normalizer,
                Utils.BuildPoint(1),
                Utils.BuildPoint(2),
                Utils.BuildPoint(3),
                Utils.BuildPoint(4));
            Sequence sequence2 = new Sequence(normalizer,
                Utils.BuildPoint(5),
                Utils.BuildPoint(6),
                Utils.BuildPoint(7),
                Utils.BuildPoint(8));

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Different sequences must not have similarity 0!");
        }

        [TestMethod]
        public void EqualLargeSequences()
        {
            Point[] values = new Point[1000];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Utils.BuildPoint(i);
            }

            Sequence sequence1 = new Sequence(normalizer, values);
            Sequence sequence2 = new Sequence(normalizer, values);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreEqual(0, similarity, "Same sequences must have similarity 0!");
        }

        [TestMethod]
        public void DifferentLargeSequences()
        {
            Point[] values1 = new Point[1000];
            Point[] values2 = new Point[1000];

            for (int i = 0; i < values1.Length; i++)
            {
                values1[i] = Utils.BuildPoint(i);
            }
            for (int i = 0; i < values2.Length; i++)
            {
                values2[i] = Utils.BuildPoint(i + values1.Length);
            }

            Sequence sequence1 = new Sequence(normalizer, values1);
            Sequence sequence2 = new Sequence(normalizer, values2);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Different sequences must not have similarity 0!");
        }
    }
}
