/// <summary>
/// TestObjectsProvider.cs
/// </summary>

namespace PLessPP.Testing.Validity.TestObjects
{
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    internal static class TestObjectsProvider
    {
        private const string sampleDataNegativeDev1FilePath = "SampleData_Negative_Dev1.csv";
        private const string sampleDataNegativeDev2FilePath = "SampleData_Negative_Dev2.csv";

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataNegativeDev1FilePath
        {
            get { return GetPath(sampleDataNegativeDev1FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataNegativeDev2FilePath
        {
            get { return GetPath(sampleDataNegativeDev2FilePath); }
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Validity", "TestObjects", fileName);
        }
    }
}
