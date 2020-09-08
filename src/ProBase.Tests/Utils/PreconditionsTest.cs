using NUnit.Framework;
using ProBase.Utils;
using System;

namespace ProBase.Tests.Utils
{
    [TestFixture]
    public class PreconditionsTest
    {
        [Test]
        public void NullValueThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Preconditions.CheckNotNull<object>(null, ParameterName), "The null check should throw an exception");
        }

        [Test]
        public void NotNullDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => Preconditions.CheckNotNull(new object(), ParameterName), "A non-null value should not throw an exception");
        }

        [Test]
        public void OutOfRangeThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Preconditions.CheckArgumentRange(ParameterName, value: 101, minInclusive: 0, maxInclusive: 100), "An out of range value should throw an exception");
        }

        [Test]
        public void InRangeDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => Preconditions.CheckArgumentRange(ParameterName, value: 50, minInclusive: 0, maxInclusive: 100), "An in range value should not throw an exception");
        }

        private const string ParameterName = "parameter";
    }
}
