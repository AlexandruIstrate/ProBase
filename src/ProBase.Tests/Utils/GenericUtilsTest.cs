using NUnit.Framework;
using ProBase.Generation;
using ProBase.Utils;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                Assert.IsTrue(TypeUtils.IsGenericTypeDefinition(typeof(IEnumerable<string>), typeof(IEnumerable<>)), "The check must return true");
            },
            "The check operation must be successful");
        }

        [Test]
        public void CanInvokeGenericMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                MethodInfo method = ClassUtils.GetMethod<GenericUtilsTest>(nameof(GenericMethod));
                TypeUtils.InvokeGenericMethod(method, new[] { typeof(string) }, this, new[] { "Parameter" });
            },
            "The call must be successful");
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "This method does not provide any functionality")]
        private void GenericMethod<T>(T param)
        {
            
        }
    }
}
