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
    using PLessPP.Data.Connectors;
    using PLessPP.Similarity;
    using PLessPP.Testing;
    using PLessPP.Testing.AI.SearchAlgorithms.TestObjects;

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
    /// 
    /// TODO: Readjust thresholds!
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
            // Declaring to MSTest which files to leave in the out folder
            // TODO: Change method names to match threshold
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_MWMST_1_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_1_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Constantin_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Constantin_Sx_NP3N_P1_A_MWMS_MWMST_1_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_3_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_Liansheng_Sx_P_A_MWMS_MWMST_1_1_100)));
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
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

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
        public void Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive2,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive2Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_1_1_100)));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        /// <summary>
        /// Constantin samples.
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
        public void Scenario_Constantin_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataConstantinPositive1, // This is actually the baseline we have at the moment
                TestObjectsProvider.SampleDataConstantinNP2N,
                1,
                TestObjectsProvider.SampleDataConstantinPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Constantin_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100)));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        /// <summary>
        /// Constantin samples.
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
        public void Scenario_Constantin_Sx_NP3N_P1_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataConstantinPositive1, // This is actually the baseline we have at the moment
                TestObjectsProvider.SampleDataConstantinNP3N,
                1,
                TestObjectsProvider.SampleDataConstantinPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1.1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Constantin_Sx_NP3N_P1_A_MWMS_MWMST_1_1_100)));

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
        /// 3 windows.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_3_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive2,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                Utils.GenerateWindowTriple(TestObjectsProvider.SampleDataAndreaPositive2Length, TestObjectsProvider.SampleDataAndreaNP1NLength),
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P2_A_MWMS_MWMST_3_1_100)));

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
        public void Scenario_Andrea_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP2N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP2N_P1_A_MWMS_MWMST_1_1_100)));

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
        public void Scenario_Andrea_Sx_NP1N_Liansheng_Sx_P_A_MWMS_MWMST_1_1_100()
        {
            object results;
            bool matchFound = GetDTWMultiWindowMultiShiftSearchDecidedResult(
                TestObjectsProvider.SampleDataLianshengPositive,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataLianshengPositiveLength,
                new AbsoluteDifferencePointDistanceCalculator(),
                new MultiWindowMultiShiftThresholdSearchDecider(1),
                out results,
                normalize: true);

            WriteResults(results, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_Liansheng_Sx_P_A_MWMS_MWMST_1_1_100)));

            Assert.AreEqual(true, matchFound, "Match expected in NPN configuration!");
        }

        private static bool GetDTWMultiWindowMultiShiftSearchDecidedResult(string baselineFile, string sequenceFile, int shift, int windowLength, 
            IPointDistanceCalculator distanceCalculator, ISearchDecider searchDecider, out object results, bool normalize = false)
        {
            return GetDTWMultiWindowMultiShiftSearchDecidedResult(baselineFile, sequenceFile, shift, new int[] { windowLength }, 
                distanceCalculator, searchDecider, out results, normalize);
        }

        private static bool GetDTWMultiWindowMultiShiftSearchDecidedResult(string baselineFile, string sequenceFile, int shift, int[] windowLengths,
            IPointDistanceCalculator distanceCalculator, ISearchDecider searchDecider, out object results, bool normalize = false)
        {
            CSVDataConnector dataConnectorBaseline = new CSVDataConnector(baselineFile);
            CSVDataConnector dataConnectorSequence = new CSVDataConnector(sequenceFile);

            Sequence baseline = dataConnectorBaseline.Data;
            Sequence sequence = dataConnectorSequence.Data;

            ISequenceSearcher mwmsSearchAlgorithm = new MultiWindowMultiShiftSearch(shift, windowLengths, baseline,
                new DynamicTimeWarpingAlgorithm(distanceCalculator), normalize);

            mwmsSearchAlgorithm.Search(sequence, out results);

            bool matchFound = searchDecider.MatchFound(results);

            return matchFound;
        }

        private static void WriteResults(object results, string identifier)
        {
            var mwmsResults = results as MultiWindowMultiShiftResults;
            Utils.WriteMWMSResults(mwmsResults, Path.Combine(Suite.OutputPath, identifier));
        }

        private static void WriteSequences(Sequence sequence, string identifier)
        {
            Utils.WriteSequence(sequence, identifier);
        }

        private string GetCompleteLogFileName(string noExtensionName)
        {
            return string.Format("{0}__{1}{2}", nameof(MultiWindowMultiShiftSearchTestSuite), noExtensionName, ".txt");
        }
    }
}
