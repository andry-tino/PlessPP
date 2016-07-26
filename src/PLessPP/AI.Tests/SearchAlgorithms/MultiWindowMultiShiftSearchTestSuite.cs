/// <summary>
/// 
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.AI;
    using PLessPP.Data;
    using PLessPP.Similarity;
    using PLessPP.Testing.AI.SearchAlgorithms.TestObjects;
    using PLessPP.Testing.Testability.Data;

    /// <summary>
    /// This test suite considers a sequence of data from real sampling and compare to a baseline.
    /// 
    /// Legend:
    /// Name is as follows: Scenario_<tester-type>_<hand>_<matching-config>_<distance-algo>_<search-algo>_<decider>_<windows-num>_<shift>
    /// - tester-type:      D    = Developer or name
    /// - hand:             Sx   = Left
    ///                     Rx   = Right
    /// - matching-config:  P    = Positive
    ///                     N    = Negative
    ///                     Might uae syntax <sequence>_<baseline>
    /// - distance-algo:    A    = Plain absolute difference
    ///                     E    = Euclidean (not squared)
    /// - search-algo:      MWMS = Multi Window Multi Shift
    /// - decider:          
    /// </summary>
    [TestClass]
    public class MultiWindowMultiShiftSearchTestSuite
    {
        /// <summary>
        /// Andrea samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// Multi Window Multi Shift Threshold decider.
        /// 1 window.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P1_A_MWMS_MWMST_1_1()
        {
            bool matchFound = GetMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(100));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        /// <summary>
        /// Andrea samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Use different positive sequence from the same person as baseline.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// Multi Window Multi Shift Threshold decider.
        /// 1 window.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_1_1()
        {
            bool matchFound = GetMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive2,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive2Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(100));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        /// <summary>
        /// Andrea samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Use different positive sequence from the same person as baseline.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// Multi Window Multi Shift Threshold decider.
        /// 1 window.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP2N_P1_A_MWMS_MWMST_1_1()
        {
            bool matchFound = GetMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP2N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(100));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        /// <summary>
        /// Left hand.
        /// Positive sequence inside negative sequence, all from Andrea's data.
        /// Use one positive sequence from Liansheng as baseline.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// Multi Window Multi Shift Threshold decider.
        /// 1 window.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_Liansheng_Sx_P_A_MWMS_MWMST_1_1()
        {
            bool matchFound = GetMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataLianshengPositive,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataLianshengPositiveLength,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(100));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        private static bool GetMultiWindowMultiShiftSearchDecidedResult(string baselineFile, string sequenceFile, int shift, int windowLength, 
            IPointDistanceCalculator distanceCalculator, ISearchDecider searchDecider, bool normalized = false)
        {
            CSVDataConnector dataConnectorBaseline = new CSVDataConnector(baselineFile);
            CSVDataConnector dataConnectorSequence = new CSVDataConnector(sequenceFile);

            Sequence baseline = normalized ? dataConnectorBaseline.Data.Normalize() : dataConnectorBaseline.Data;
            Sequence sequence = normalized ? dataConnectorSequence.Data.Normalize() : dataConnectorSequence.Data;

            ISequenceSearcher mwmsSearchAlgorithm = new MultiWindowMultiShiftSearch(shift, new int[] { windowLength }, baseline, new DynamicTimeWarpingAlgorithm(distanceCalculator));

            object results;
            mwmsSearchAlgorithm.Search(sequence, out results);

            bool matchFound = searchDecider.MatchFound(results);

            return matchFound;
        }
    }
}
