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
        private const string sampleDataPositiveDev31FilePath = "SampleData_Positive_Dev3_1.csv";
        private const string sampleDataPositiveDev32FilePath = "SampleData_Positive_Dev3_2.csv";
        private const string sampleDataPositiveDev4FilePath = "SampleData_Positive_Dev4.csv";
        private const string sampleDataPositiveDev5FilePath = "SampleData_Positive_Dev5.csv";
        private const string sampleDataPositiveAndrea2FilePath = "SampleData_Positive_Andrea_2.csv";
        private const string sampleDataPositiveLiansheng2FilePath = "SampleData_Positive_Liansheng_2.csv";

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

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveDev31FilePath
        {
            get { return GetPath(sampleDataPositiveDev31FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveDev32FilePath
        {
            get { return GetPath(sampleDataPositiveDev32FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveDev4FilePath
        {
            get { return GetPath(sampleDataPositiveDev4FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveDev5FilePath
        {
            get { return GetPath(sampleDataPositiveDev5FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveAndrea2FilePath
        {
            get { return GetPath(sampleDataPositiveAndrea2FilePath); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataPositiveLiansheng2FilePath
        {
            get { return GetPath(sampleDataPositiveLiansheng2FilePath); }
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Validity", "TestObjects", fileName);
        }
    }
}
