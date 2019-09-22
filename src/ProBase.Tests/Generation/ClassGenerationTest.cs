using NUnit.Framework;
using ProBase.Generation;
using System;

namespace ProBase.Tests.Generation
{
    [TestFixture]
    public class ClassGenerationTest
    {
        [Test]
        public void CanCreateClassInstance()
        {
            IConcreteClassGenerator classGenerator = null;

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
            IConcreteClassGenerator classGenerator = ClassGeneratorFactory.Create();
            Type generatedType = null;

            Assert.DoesNotThrow(() =>
            {
                generatedType = classGenerator.GenerateClassImplementingInterface(typeof(IGenerationTestInterface));
            },
            "The generation of the concrete type must be successful");

            Assert.NotNull(generatedType, "The generator must return a non-null value");
        }
    }
}
