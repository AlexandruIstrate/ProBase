using NUnit.Framework;
using ProBase.Generation;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation
{
    [TestFixture]
    public class ConstructorGenerationTest
    {
        [OneTimeSetUp]
        public void GeneralSetUp()
        {
            Assert.DoesNotThrow(() =>
            {
                constructorGenerator = new DatabaseConstructorGenerator();
            }, "The creation of the constuctor generator must be successful");
        }

        //[Test]
        //public void CanGenerateDefaultConstructor()
        //{
        //    constructorGenerator.GenerateDefaultConstructor(new Dictionary<Type, ValueType>(), GetTypeBuilder());
        //}

        //[Test]
        //public void CanGenerateDependencyConstructor()
        //{
        //    constructorGenerator.GenerateDependencyConstructor(null, GetTypeBuilder());
        //}

        private TypeBuilder GetTypeBuilder()
        {
            throw new NotImplementedException();
        }

        private IConstructorGenerator constructorGenerator;
    }
}
