/// <summary>
/// DTWSequenceMatching.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [TestMethod]
        public void Scenario_D_Sx_NxN()
        {
            CSVDataConnector dataConnector1 = new CSVDataConnector(TestObjectsProvider.SampleDataNegativeDev1FilePath);
            CSVDataConnector dataConnector2 = new CSVDataConnector(TestObjectsProvider.SampleDataNegativeDev2FilePath);

            Sequence sequence1 = dataConnector1.Data;
            Sequence sequence2 = dataConnector2.Data;
        }
    }
}
