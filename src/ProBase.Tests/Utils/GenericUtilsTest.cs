using NUnit.Framework;
using ProBase.Generation;
using ProBase.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace ProBase.Tests.Utils
{
    [TestFixture]
    public class GenericUtilsTest
    {
        [Test]
        public void CanCheckIsGenericTypeDefinition()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.IsTrue(GenericUtils.IsGenericTypeDefinition(typeof(IEnumerable<string>), typeof(IEnumerable<>)), "The check must return true");
            },
            "The check operation must be successful");
        }

        [Test]
        public void CanInvokeGenericMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                MethodInfo method = GeneratedClass.GetMethod<GenericUtilsTest>(nameof(GenericMethod));
                GenericUtils.InvokeGenericMethod(method, new[] { typeof(string) }, this, new[] { "Parameter" });
            },
            "The call must be successful");
        }

        private void GenericMethod<T>(T param)
        {
            
        }
    }
}
