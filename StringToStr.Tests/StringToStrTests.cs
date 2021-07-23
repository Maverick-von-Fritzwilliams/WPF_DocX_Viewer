using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringToStr.Tests
{
    [TestClass]
    public class StringToStrTests
    {
        [TestMethod]
        public void SizeToStr_1000000_1MBreturned()
        {
            // arrange
            long lsize = 1000000;
            string expected = "1MB";

            // act
            string actual = SizeToStrlib.SizeToStrlib.Size_ToStr(lsize);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
