using System.Numerics;

namespace Exercise01.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReturnsValidWordRepresentationOfInt()
        {
            var input = 18000000;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfUInt()
        {
            var input = 18000000u;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfLong()
        {
            var input = 18000000L;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfULong()
        {
            var input = 18000000ul;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfFloat()
        {
            var input = 18000000f;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfDouble()
        {
            var input = 18000000d;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfDecimal()
        {
            var input = 18000000m;
            Assert.AreEqual("eighteen million", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfBigInteger()
        {
            var input = BigInteger.Parse("18456002032011000007");
            Assert.AreEqual("eighteen quintillion, four hundred and fifty-six quadrillion, two trillion, thirty-two billion, eleven million, and seven", input.Towards());
        }

        [TestMethod]
        public void ReturnsValidWordRepresentationOfNegativeInt()
        {
            var input = -18000000;
            Assert.AreEqual("negative eighteen million", input.Towards());
        }
    }
}