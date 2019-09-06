using NUnit.Framework;

namespace ProBase.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void BasicTest()
        {
            Assert.AreEqual(2, 2, "These two numbers should always be equal");
        }
    }
}
