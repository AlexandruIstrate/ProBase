using System;
using System.Reflection.Emit;
using NUnit.Framework;
using ProBase.Generation;
using ProBase.Generation.Class;
using ProBase.Tests.Substitutes;

namespace ProBase.Tests.Generation.Class
{
    [TestFixture]
    public class FieldGenerationTest
    {
        [OneTimeSetUp]
        public void GeneralSetUp()
        {
            Assert.DoesNotThrow(() =>
            {
                fieldGenerator = new DbFieldGenerator();
            },
            "The creation of the field generator must be successful");
        }

        [Test]
        public void CanGenerateField()
        {
            Assert.DoesNotThrow(() =>
            {
                fieldGenerator.GenerateField("testField", typeof(string), GetTypeBuilder());
            },
            "The creation of the field must be successful");
        }

        private TypeBuilder GetTypeBuilder()
        {
            return GenerationUtils.GetTypeBuilder(typeof(IMixedOperations));
        }

        private IClassFieldGenerator fieldGenerator;
    }
}
