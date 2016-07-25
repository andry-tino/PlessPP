/// <summary>
/// DTWPropertiesTestSuite.cs
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
    public class DTWPropertiesTestSuite
    {
        [TestMethod]
        public void SimilarityIsPositiveNumber()
        {
            Sequence sequence1 = new Sequence(1, 2, 3, 4);
            Sequence sequence2 = new Sequence(5, 6, 7, 8);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity1 = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);
            double similarity2 = dtwAlgorithm.ComputeSimilarity(sequence2, sequence1);
            
            Assert.IsTrue(similarity1 > 0, "Similarity expected to be positive!");
            Assert.IsTrue(similarity2 > 0, "Similarity expected to be positive!");
        }

        [TestMethod]
        public void SimilarityIsComutative()
        {
            Sequence sequence1 = new Sequence(1, 2, 3, 4);
            Sequence sequence2 = new Sequence(5, 6, 7, 8);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity1 = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);
            double similarity2 = dtwAlgorithm.ComputeSimilarity(sequence2, sequence1);

            Assert.AreEqual(similarity1, similarity2, "Similarity expected not to be the same!");
        }
    }
}
