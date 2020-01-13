using NUnit.Framework;
using ProBase.Attributes;
using ProBase.Generation.Method;
using ProBase.Tests.Substitutes;
using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Operations
{
    [Ignore("Not adapted")]
    [TestFixture]
    public class ProcedureCallGeneratorTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            procedureCallGenerator = new ProcedureCallGenerator();
        }

        [Test]
        public void CanGenerateScalarMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                //procedureCallGenerator.Generate(typeof(DataSet), ProcedureType.Automatic, CreateScalarMethod());
            },
            "The scalar call must be generated successfuly");
        }

        [Test]
        public void CanGenerateNonQueryMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                //procedureCallGenerator.Generate(typeof(int), ProcedureType.Automatic, CreateNonQueryMethod());
            },
            "The non query call must be generated successfuly");
        }

        private ILGenerator CreateScalarMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("ScalarMethod", MethodAttributes.Public, typeof(DataSet), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ILGenerator CreateNonQueryMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("NonQueryMethod", MethodAttributes.Public, typeof(int), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ProcedureCallGenerator procedureCallGenerator;
    }
}
