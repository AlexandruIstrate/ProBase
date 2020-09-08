using NUnit.Framework;
using ProBase.Generation;
using ProBase.Generation.Class;
using ProBase.Tests.Substitutes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Class
{
    [TestFixture]
    public class ConstructorGenerationTest
    {
        [OneTimeSetUp]
        public void GeneralSetUp()
        {
            Assert.DoesNotThrow(() =>
            {
                constructorGenerator = new DbConstructorGenerator();
            },
            "The creation of the constuctor generator must be successful");
        }

        [Test]
        public void CanGenerateDefaultConstructor()
        {
            Assert.DoesNotThrow(() =>
            {
                constructorGenerator.GenerateDefaultConstructor(new Dictionary<Type, ValueType>(), GetTypeBuilder());
            },
            "The generation of the default constructor must be successful");
        }

        [Test]
        public void CanGenerateDependencyConstructor()
        {
            Assert.DoesNotThrow(() =>
            {
                constructorGenerator.GenerateDependencyConstructor(new FieldInfo[] { }, GetTypeBuilder());
            },
            "The generation of the dependency constructor must be successful");
        }

        private TypeBuilder GetTypeBuilder()
        {
            return GenerationUtils.GetTypeBuilder(typeof(IMixedOperations));
        }

        private IConstructorGenerator constructorGenerator;
    }
}
