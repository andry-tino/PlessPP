/// <summary>
/// DTWPropertiesTestSuite.cs
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
    public class DTWPropertiesTestSuite
    {
        private INormalizer normalizer = new SimpleNormalizer();
        [TestMethod]
        public void SimilarityIsPositiveNumber()
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

            double similarity1 = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);
            double similarity2 = dtwAlgorithm.ComputeSimilarity(sequence2, sequence1);
            
            Assert.IsTrue(similarity1 > 0, "Similarity expected to be positive!");
            Assert.IsTrue(similarity2 > 0, "Similarity expected to be positive!");
        }

        [TestMethod]
        public void SimilarityIsComutative()
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

            double similarity1 = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);
            double similarity2 = dtwAlgorithm.ComputeSimilarity(sequence2, sequence1);

            Assert.AreEqual(similarity1, similarity2, "Similarity expected not to be the same!");
        }
    }
}
