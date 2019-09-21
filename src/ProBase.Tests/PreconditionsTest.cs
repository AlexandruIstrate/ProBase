using NUnit.Framework;
using ProBase.Utils;
using System;

namespace ProBase.Tests
{
    [TestFixture]
    public class PreconditionsTest
    {
        [Test]
        public void NullValueThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Preconditions.CheckNotNull<object>(null, "parameter"), "The null check should throw an exception");
        }

        [Test]
        public void NotNullDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => Preconditions.CheckNotNull(new object(), "parameter"), "A non-null value should not throw an exception");
        }
    }
}
