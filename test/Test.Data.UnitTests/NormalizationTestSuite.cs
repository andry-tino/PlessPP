/**
 * NormalizationTestSuite.cs
 * 
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 */

namespace PlessPP.Testing.Data
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PLessPP.Data;

    [TestClass]
    public class NormalizationTestSuite
    {
        private SimpleNormalizer simpleNormalizer = new SimpleNormalizer();
        [TestMethod]
        public void TestMethod1()
        {
            Sequence seq = new Sequence(this.simpleNormalizer,
                new Point(0, 1, 1, 3, 10, 100, 1),
                new Point(1, 2, 0, 3, 1, 50, 2),
                new Point(2, 3, -1, 2, 2, 10, 3),
                new Point(3, 4, -2, 3, 3, 5, 4),
                new Point(4, 5, -3, 3, 4, 1, 5));

            Assert.IsNotNull(seq.Normalize());
        }
    }
}
