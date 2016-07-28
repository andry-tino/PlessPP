/// <summary>
/// TestObjectsProvider.cs
/// </summary>

namespace PLessPP.Testing.Data.UnitTests.TestObjects
{
    using System;
    using System.IO;

    internal static class TestObjectsProvider
    {
        private const string sampleDataLianshengPositive = "SampleData_Liansheng_Positive.csv";

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
            return Path.Combine(Directory.GetCurrentDirectory(), "TestObjects", fileName);
        }
    }
}
