/**
 * TestObjectsProvider.cs
 * 
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 */

namespace PLessPP.Testing.AI.SearchAlgorithms.TestObjects
{
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    internal static class TestObjectsProvider
    {
        private const string sampleDataAndreaNegative = "SampleData_Andrea_Negative.csv";
        private const string sampleDataAndreaNP1N = "SampleData_Andrea_NP1N.csv";
        private const string sampleDataConstantinNP2N = "SampleData_Constantin_NP2N.csv";
        private const string sampleDataConstantinNP3N = "SampleData_Constantin_NP3N.csv";
        private const string sampleDataAndreaNP2N = "SampleData_Andrea_NP2N.csv";
        private const string sampleDataAndreaPositive1 = "SampleData_Andrea_Positive_1.csv";
        private const string sampleDataConstantinPositive1 = "SampleData_Constantin_Positive_1.csv";
        private const string sampleDataAndreaPositive2 = "SampleData_Andrea_Positive_2.csv";
        private const string sampleDataLianshengPositive = "SampleData_Liansheng_Positive.csv";

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataAndreaNegative
        {
            get { return GetPath(sampleDataAndreaNegative); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataAndreaNP1N
        {
            get { return GetPath(sampleDataAndreaNP1N); }
        }

        /// <summary>
        /// 
        /// </summary>
        public const int SampleDataAndreaNP1NLength = 1455;

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataConstantinNP2N
        {
            get { return GetPath(sampleDataConstantinNP2N); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataConstantinNP3N
        {
            get { return GetPath(sampleDataConstantinNP3N); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataAndreaNP2N
        {
            get { return GetPath(sampleDataAndreaNP2N); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataAndreaPositive1
        {
            get { return GetPath(sampleDataAndreaPositive1); }
        }

        /// <summary>
        /// 
        /// </summary>
        public const int SampleDataAndreaPositive1Length = 103;

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataConstantinPositive1
        {
            get { return GetPath(sampleDataConstantinPositive1); }
        }

        /// <summary>
        /// 
        /// </summary>
        public const int SampleDataConstantinPositive1Length = 74;

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataAndreaPositive2
        {
            get { return GetPath(sampleDataAndreaPositive2); }
        }

        /// <summary>
        /// 
        /// </summary>
        public const int SampleDataAndreaPositive2Length = 87;

        /// <summary>
        /// 
        /// </summary>
        public const int SampleDataLianshengPositiveLength = 103;

        /// <summary>
        /// 
        /// </summary>
        public static string SampleDataLianshengPositive => GetPath(sampleDataLianshengPositive);

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "SearchAlgorithms", "TestObjects", fileName);
        }
    }
}
