/**
 * MultiWindowMultiShiftSearchTimingTestSuite.cs
 * 
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 */

namespace PLessPP.Testing.MWMSSearchAlgorithm
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PLessPP.AI.MWMSSearchAlgorithm;
    using PLessPP.Data;
    using PLessPP.Data.Connectors;
    using PLessPP.AI.Similarity;
    using PLessPP.Testing;
    using PLessPP.Testing.AI.SearchAlgorithms.TestObjects;

    /// <summary>
    /// This test suite considers a sequence of data from real sampling and compare to a baseline and checking times.
    /// 
    /// Legend:
    /// Name is as follows: Scenario_<tester-type>_<hand>_<matching-config>_<distance-algo>_<search-algo>_<windows-num>_<shift>
    /// - tester-type:      D     = Developer or name
    /// - hand:             Sx    = Left
    ///                     Rx    = Right
    /// - matching-config:  P     = Positive
    ///                     N     = Negative
    ///                     Might use syntax <sequence>_<baseline>
    /// - distance-algo:    A     = Plain absolute difference
    ///                     E     = Euclidean (not squared)
    /// - search-algo:      MWMS  = Multi Window Multi Shift
    /// - windows-num:      Number of windows being used
    /// - shift:            Shift to use
    /// </summary>
    [TestClass]
    public class MultiWindowMultiShiftSearchTimingTestSuite
    {
        /// <summary>
        /// Initializes file results.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Declaring to MSTest which files to leave in the out folder
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_1)));
            Suite.AddOutputFile(GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_10)));
        }

        /// <summary>
        /// Andrea's samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Baseline is the same positive subsequence in the sequence.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// 1 window.
        /// Shift: 1 point.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_1()
        {
            var record = PerformDTWMultiWindowMultiShiftSearch(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP1N,
                1,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                normalize: true);

            WriteTiming(record, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_1)));

            Assert.AreNotEqual(0, record.ElapsedTime, "Elapsed time out of valid range!");
        }

        /// <summary>
        /// Andrea's samples.
        /// Left hand.
        /// Positive sequence inside negative sequence.
        /// Baseline is the same positive subsequence in the sequence.
        /// Absolute difference distance.
        /// Multi Window Multi Shift search algorithm.
        /// 1 window.
        /// Shift: 10 points.
        /// </summary>
        [TestMethod]
        public void Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_10()
        {
            var record = PerformDTWMultiWindowMultiShiftSearch(
                TestObjectsProvider.SampleDataAndreaPositive1,
                TestObjectsProvider.SampleDataAndreaNP1N,
                10,
                TestObjectsProvider.SampleDataAndreaPositive1Length,
                new AbsoluteDifferencePointDistanceCalculator(),
                normalize: true);

            WriteTiming(record, GetCompleteLogFileName(nameof(Scenario_Andrea_Sx_NP1N_P1_A_MWMS_1_10)));

            Assert.AreNotEqual(0, record.ElapsedTime, "Elapsed time out of valid range!");
        }

        private static Utils.TimingRecord PerformDTWMultiWindowMultiShiftSearch(string baselineFile, string sequenceFile, int shift, int windowLength,
            IPointDistanceCalculator distanceCalculator, bool normalize = false)
        {
            CSVDataConnector dataConnectorBaseline = new CSVDataConnector(baselineFile);
            CSVDataConnector dataConnectorSequence = new CSVDataConnector(sequenceFile);

            Sequence baseline = dataConnectorBaseline.Data;
            Sequence sequence = dataConnectorSequence.Data;

            ISequenceSearcher mwmsSearchAlgorithm = new MultiWindowMultiShiftSearch(shift, new int[] { windowLength }, baseline,
                new DynamicTimeWarpingAlgorithm(distanceCalculator), normalize);

            object results;

            var record = Utils.TimingRecord.Create(); // Start timing evaluation
            mwmsSearchAlgorithm.Search(sequence, out results);
            record.StopWatch(); // Stop timing evaluation

            return record;
        }

        private static void WriteTiming(Utils.TimingRecord record, string identifier)
        {
            Utils.WriteTimingRecord(record, Path.Combine(Suite.OutputPath, identifier));
        }

        private string GetCompleteLogFileName(string noExtensionName)
        {
            return string.Format("{0}__{1}{2}", nameof(MultiWindowMultiShiftSearchTimingTestSuite), noExtensionName, ".txt");
        }
    }
}
