/// <summary>
/// Suite.cs
/// </summary>

namespace PLessPP.Testing
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public static class Suite
    {
        internal static string OutputPath;

        private static TestContext testContext;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            testContext = context;

            OutputPath = context.DeploymentDirectory;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }

        internal static void AddOutputFile(string file)
        {
            testContext.AddResultFile(file);
        }
    }
}
