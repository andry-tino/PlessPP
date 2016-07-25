/// <summary>
/// DTWSequenceMatching.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.Data;
    using PLessPP.Similarity.Data;
    using PLessPP.Similarity;
    using PLessPP.Testing.Testability.Data;
    using PLessPP.Testing.Validity.TestObjects;
    
    /// <summary>
    /// This test suite considers twi sequences of data from real sampling and compares them.
    /// 
    /// Legend:
    /// Name is as follows: Scenario_<tester-type>_<hand>_<matching-config>
    /// - tester-type:      D  = Developer
    /// - hand:             Sx = Left
    ///                     Rx = Right
    /// - matching-config:  P  = Positive
    ///                     N  = Negative
    /// </summary>
    [TestClass]
    public class DTWSequenceMatchingTestSuite
    {
        /// <summary>
        /// Two different developers.
        /// Two negative sequences.
        /// Left hand.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_NxN()
        {
            CSVDataConnector dataConnector1 = new CSVDataConnector(TestObjectsProvider.SampleDataNegativeDev1FilePath);
            CSVDataConnector dataConnector2 = new CSVDataConnector(TestObjectsProvider.SampleDataNegativeDev2FilePath);

            Sequence sequence1 = dataConnector1.Data;
            Sequence sequence2 = dataConnector2.Data;

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// One developer.
        /// Two positive sequences.
        /// Left hand.
        /// </summary>
        [TestMethod]
        public void Scenario_D1_Sx_PxP()
        {
            CSVDataConnector dataConnector1 = new CSVDataConnector(TestObjectsProvider.SampleDataPositiveDev31FilePath);
            CSVDataConnector dataConnector2 = new CSVDataConnector(TestObjectsProvider.SampleDataPositiveDev32FilePath);

            Sequence sequence1 = dataConnector1.Data;
            Sequence sequence2 = dataConnector2.Data;

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two positive sequences.
        /// Left hand.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_PxP()
        {
            CSVDataConnector dataConnector1 = new CSVDataConnector(TestObjectsProvider.SampleDataPositiveDev4FilePath);
            CSVDataConnector dataConnector2 = new CSVDataConnector(TestObjectsProvider.SampleDataPositiveDev5FilePath);

            Sequence sequence1 = dataConnector1.Data;
            Sequence sequence2 = dataConnector2.Data;

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(
                new AbsoluteDifferencePointDistanceCalculator());

            double similarity = dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }
    }
}
