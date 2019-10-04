using NUnit.Framework;
using ProBase.Data;
using ProBase.Generation;
using ProBase.Generation.Converters;
using ProBase.Generation.Operations;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation
{
    [TestFixture]
    public class MethodGenerationTest
    {
        [OneTimeSetUp]
        public void GeneralSetup()
        {
            Assert.DoesNotThrow(() =>
            {
                methodGenerator = new DatabaseMethodGenerator(new ParameterArrayGenerator(new ParameterInfoConverter()), new ProcedureCallGenerator());
            },
            "The creation of the method generator must be successful");
        }

        [Test]
        public void CanGenerateMethodImplementation()
        {
            foreach (MethodInfo method in typeof(IGenerationTestInterface).GetMethods())
            {
                MethodBuilder methodBuilder = null;

                Assert.DoesNotThrow(() =>
                {
                    TypeBuilder typeBuilder = GetTypeBuilder();
                    methodBuilder = methodGenerator.GenerateMethod(method, new FieldInfo[] { GetDatabaseMapperField(typeBuilder) }, typeBuilder);
                },
                "The method generation must be successful");

                Assert.NotNull(methodBuilder, "The method generator must return a non-null value");
            }
        }

        private TypeBuilder GetTypeBuilder()
        {
            return GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
        }

        private FieldInfo GetDatabaseMapperField(TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineField("procedureMapper", typeof(IProcedureMapper), FieldAttributes.Private | FieldAttributes.InitOnly);
        }

        private IMethodGenerator methodGenerator;
    }
}
