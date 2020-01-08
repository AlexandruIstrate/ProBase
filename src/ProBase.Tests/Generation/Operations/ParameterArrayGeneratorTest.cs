using NUnit.Framework;
using ProBase.Generation.Converters;
using ProBase.Generation.Operations;
using ProBase.Tests.Substitutes;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Operations
{
    [Ignore("Not finished")]
    [TestFixture]
    public class ParameterArrayGeneratorTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            arrayGenerator = new ParameterArrayGenerator(new ParameterInfoConverter());
        }

        [Test]
        public void CanGenerate()
        {
            Assert.DoesNotThrow(() =>
            {
                arrayGenerator.Generate(new ParameterInfo[0], new FieldInfo[0], CreateTestMethod());
            },
            "The generation operation must be successful");
        }

        private ILGenerator CreateTestMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("TestMethod", MethodAttributes.Public, typeof(void), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ParameterArrayGenerator arrayGenerator;
    }
}
