using NUnit.Framework;
using ProBase.Attributes;
using ProBase.Generation.Operations;
using ProBase.Tests.Substitutes;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Operations
{
    [TestFixture]
    public class ProcedureCallGeneratorTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            procedureCallGenerator = new ProcedureCallGenerator();
        }

        [Test]
        public void CanGenerateBasedOnReturnType()
        {

        }
        
        [Test]
        public void CanGenerateBasedOnProcedureTypeScalar()
        {
            Assert.DoesNotThrow(() =>
            {
                procedureCallGenerator.Generate(ProcedureType.Scalar, CreateScalarMethod());
            });
        }

        [Test]
        public void CanGenerateBasedOnProcedureTypeNonQuery()
        {
            Assert.DoesNotThrow(() =>
            {
                procedureCallGenerator.Generate(ProcedureType.NonQuery, CreateNonQueryMethod());
            });
        }

        private ILGenerator CreateTestMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("TestMethod", MethodAttributes.Public, typeof(void), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ILGenerator CreateScalarMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IGenerationTestInterface));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("ScalarMethod", MethodAttributes.Public, typeof(void), new Type[0]);
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
