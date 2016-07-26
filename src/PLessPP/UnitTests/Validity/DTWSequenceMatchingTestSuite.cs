/// <summary>
/// DTWSequenceMatching.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.Data;
    using PLessPP.Similarity;
    using PLessPP.Testing.Testability.Data;
    using PLessPP.Testing.Validity.TestObjects;

    /// <summary>
    /// This test suite considers twi sequences of data from real sampling and compares them.
    /// 
    /// Legend:
    /// Name is as follows: Scenario_<tester-type>_<hand>_<matching-config>_<distance-algo>
    /// - tester-type:      D  = Developer
    /// - hand:             Sx = Left
    ///                     Rx = Right
    /// - matching-config:  P  = Positive
    ///                     N  = Negative
    /// - distance-algo:    A  = Plain absolute difference
    ///                     E  = Euclidean (not squared)
    /// </summary>
    [TestClass]
    public class DTWSequenceMatchingTestSuite
    {
        /// <summary>
        /// Two different developers.
        /// Two negative sequences.
        /// Left hand.
        /// Absolute distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_NxN_A()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataNegativeDev1FilePath,
                TestObjectsProvider.SampleDataNegativeDev2FilePath,
                new AbsoluteDifferencePointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two negative sequences.
        /// Left hand.
        /// Euclidean distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_NxN_E()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataNegativeDev1FilePath,
                TestObjectsProvider.SampleDataNegativeDev2FilePath,
                new EuclideanPointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// One negative sequence.
        /// One positive sequence.
        /// Left hand.
        /// Absolute distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_NxP_A()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataNegativeDev1FilePath,
                TestObjectsProvider.SampleDataPositiveDev31FilePath,
                new AbsoluteDifferencePointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// One negative sequence.
        /// One positive sequence.
        /// Left hand.
        /// Euclidean distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_NxP_E()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataNegativeDev1FilePath,
                TestObjectsProvider.SampleDataPositiveDev31FilePath,
                new EuclideanPointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// One developer.
        /// Two positive sequences.
        /// Left hand.
        /// Absolute distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1_Sx_PxP_A()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveDev31FilePath,
                TestObjectsProvider.SampleDataPositiveDev32FilePath,
                new AbsoluteDifferencePointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// One developer.
        /// Two positive sequences.
        /// Left hand.
        /// Euclidean distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1_Sx_PxP_E()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveDev31FilePath,
                TestObjectsProvider.SampleDataPositiveDev32FilePath,
                new EuclideanPointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two positive sequences.
        /// Left hand.
        /// Absolute distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_PxP_A()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveDev4FilePath, 
                TestObjectsProvider.SampleDataPositiveDev5FilePath,
                new AbsoluteDifferencePointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two positive sequences.
        /// Left hand.
        /// Euclidean distance.
        /// </summary>
        [TestMethod]
        public void Scenario_D1xD2_Sx_PxP_E()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveDev4FilePath,
                TestObjectsProvider.SampleDataPositiveDev5FilePath,
                new EuclideanPointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two positive sequences.
        /// Left hand.
        /// Absolute distance.
        /// </summary>
        [TestMethod]
        public void Scenario_AndreaxLiansheng_Sx_PxP_A()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveAndrea2FilePath,
                TestObjectsProvider.SampleDataPositiveLiansheng2FilePath,
                new AbsoluteDifferencePointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        /// <summary>
        /// Two different developers.
        /// Two positive sequences.
        /// Left hand.
        /// Euclidean distance.
        /// </summary>
        [TestMethod]
        public void Scenario_AndreaxLiansheng_Sx_PxP_E()
        {
            double similarity = GetDistance(
                TestObjectsProvider.SampleDataPositiveAndrea2FilePath,
                TestObjectsProvider.SampleDataPositiveLiansheng2FilePath,
                new EuclideanPointDistanceCalculator());

            Assert.AreNotEqual(0, similarity, "Two negative sequences from different testers should not give zero similarity!");
        }

        private static double GetDistance(string file1, string file2, IPointDistanceCalculator distanceCalculator)
        {
            CSVDataConnector dataConnector1 = new CSVDataConnector(file1);
            CSVDataConnector dataConnector2 = new CSVDataConnector(file2);

            Sequence sequence1 = dataConnector1.Data;
            Sequence sequence2 = dataConnector2.Data;

            ISimilarityAlgorithm dtwAlgorithm = new DynamicTimeWarpingAlgorithm(distanceCalculator);

            return dtwAlgorithm.ComputeSimilarity(sequence1, sequence2);
        }
    }
}
