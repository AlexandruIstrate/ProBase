using NUnit.Framework;
using ProBase.Generation.Class;
using ProBase.Tests.Substitutes;
using System;

namespace ProBase.Tests.Generation.Class
{
    [TestFixture]
    public class ClassGenerationTest
    {
        [OneTimeSetUp]
        public void GeneralSetUp()
        {
            Assert.DoesNotThrow(() =>
            {
                classGenerator = ClassGeneratorFactory.Create();
            },
            "The call to the factory method must be successful");

            Assert.NotNull(classGenerator, "The factory method must return a non-null value");
        }

        [Test]
        public void CanGenerateInterfaceImplementation()
        {
            Type generatedType = null;

            Assert.DoesNotThrow(() =>
            {
                generatedType = classGenerator.GenerateClassImplementingInterface(typeof(IGenerationTestInterface));
            },
            "The generation of the concrete type must be successful");

            Assert.NotNull(generatedType, "The generator must return a non-null value");
        }

        private IConcreteClassGenerator classGenerator;
    }
}
