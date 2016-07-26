/// <summary>
/// MultiWindowMultiShiftSearchTestSuite.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.AI;
    using PLessPP.Data;
    using PLessPP.Similarity;
    using PLessPP.Testing;
    using PLessPP.Testing.AI.SearchAlgorithms.TestObjects;
    using PLessPP.Testing.Testability.Data;

    /// <summary>
    /// This test suite considers a sequence of data from real sampling and compare to a baseline.
    /// 
    /// Legend:
    /// Name is as follows: Scenario_<tester-type>_<hand>_<matching-config>_<distance-algo>_<search-algo>_<decider>_<windows-num>_<shift>_<threshold>
    /// - tester-type:      D     = Developer or name
    /// - hand:             Sx    = Left
    ///                     Rx    = Right
    /// - matching-config:  P     = Positive
    ///                     N     = Negative
    ///                     Might use syntax <sequence>_<baseline>
    /// - distance-algo:    A     = Plain absolute difference
    ///                     E     = Euclidean (not squared)
    /// - search-algo:      MWMS  = Multi Window Multi Shift
    /// - decider:          MWMST = Multi Window Multi Shift Threshold
    /// - windows-num:      Number of windows being used
    /// - shift:            Shift to use
    /// - threshold:        Threshold to use
    /// </summary>
    [TestClass]
    public class MultiWindowMultiShiftSearchTestSuite
    {
        /// <summary>
        /// Initializes file results.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_MWMST_1_1_100)));
        }

        /// <summary>
        /// Andrea's samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Baseline is the same positive subsequence in the sequence.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// Multi Window Multi Shift Threshold decider.
        /// 1 window.
        /// Shift: 1 point.
        /// Threshold set to 100 (distamce).
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P1_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(100),
                out results);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_MWMST_1_1_100)));

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

        private static bool GetDTWMultiWindowMultiShiftSearchDecidedResult(string baselineFile, string sequenceFile, int shift, int windowLength, 
            IPointDistanceCalculator distanceCalculator, ISearchDecider searchDecider, out object results)
        {
            CSVDataConnector dataConnectorBaseline = new CSVDataConnector(baselineFile);
            CSVDataConnector dataConnectorSequence = new CSVDataConnector(sequenceFile);

            Sequence baseline = dataConnectorBaseline.Data;
            Sequence sequence = dataConnectorSequence.Data;

            ISequenceSearcher mwmsSearchAlgorithm = new MultiWindowMultiShiftSearch(shift, new int[] { windowLength }, baseline, new DynamicTimeWarpingAlgorithm(distanceCalculator));

            object results;
            mwmsSearchAlgorithm.Search(sequence, out results);

            bool matchFound = searchDecider.MatchFound(results);

            return matchFound;
        }

        private static void WriteResults(object results, string identifier)
        {
            var mwmsResults = results as MultiWindowMultiShiftResults;
            Utils.WriteMWMSResults(mwmsResults, Path.Combine(Suite.OutputPath, identifier));
        }

        private string GetCompleteLogFileName(string noExtensionName)
        {
            return string.Format("{0}{1}", noExtensionName, ".txt");
        }
    }
}
