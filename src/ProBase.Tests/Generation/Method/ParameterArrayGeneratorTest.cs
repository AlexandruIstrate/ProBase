using NUnit.Framework;
using ProBase.Generation.Converters;
using ProBase.Generation.Method;
using ProBase.Tests.Substitutes;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Method
{
    [TestFixture]
    public class ParameterArrayGeneratorTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            arrayGenerator = new ParameterArrayGenerator(new ParameterInfoConverter());
        }

        [Test]
        public void ThrowsIfNoFields()
        {
            Assert.Throws<Exception>(() =>
            {
                arrayGenerator.Generate(new ParameterInfo[0], new FieldInfo[0], CreateTestMethod());
            },
            "An exception should be thrown");
        }

        private ILGenerator CreateTestMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IMixedOperations));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("TestMethod", MethodAttributes.Public, typeof(void), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ParameterArrayGenerator arrayGenerator;
    }
}
