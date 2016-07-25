/// <summary>
/// 
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
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EqualSmallSequences()
        {
            Sequence sequence1 = new Sequence(1, 2, 3, 4);
            Sequence sequence2 = new Sequence(1, 2, 3, 4);

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm();

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreEqual(0, similarity, "Same sequences must have similarity 0!");
        }
    }
}
