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
    /// - tester-type:      D  = Developer or name
    /// - hand:             Sx = Left
    ///                     Rx = Right
    /// - matching-config:  P  = Positive
    ///                     N  = Negative
    /// - distance-algo:    A  = Plain absolute difference
    ///                     E  = Euclidean (not squared)
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
        public void Scenario_Andrea_Sx_NPN_A_MWMS_MWMST_1_1()
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

        private static bool GetMultiWindowMultiShiftSearchDecidedResult(string baselineFile, string sequenceFile, int shift, int windowLength, 
            IPointDistanceCalculator distanceCalculator, ISearchDecider searchDecider)
        {
            CSVDataConnector dataConnectorBaseline = new CSVDataConnector(baselineFile);
            CSVDataConnector dataConnectorSequence = new CSVDataConnector(sequenceFile);

            Sequence baseline = dataConnectorBaseline.Data;
            Sequence sequence = dataConnectorSequence.Data;

            ISequenceSearcher mwmsSearchAlgorithm = new MultiWindowMultiShiftSearch(1, new int[] { windowLength }, baseline, new DynamicTimeWarpingAlgorithm(distanceCalculator));

            object results;
            mwmsSearchAlgorithm.Search(sequence, out results);

            bool matchFound = searchDecider.MatchFound(results);

            return matchFound;
        }
    }
}
