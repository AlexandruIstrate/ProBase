using NUnit.Framework;
using ProBase.Attributes;
using ProBase.Generation.Method;
using ProBase.Tests.Substitutes;
using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation.Method
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
        public void CanGenerateScalarMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                procedureCallGenerator.Generate("ScalarProcedure", typeof(DataSet), ProcedureType.Scalar, null, new FieldInfo[0], CreateScalarMethod());
            },
            "The scalar call must be generated successfully");
        }

        [Test]
        public void CanGenerateNonQueryMethod()
        {
            Assert.DoesNotThrow(() =>
            {
                procedureCallGenerator.Generate("NonQueryProcedure", typeof(DataSet), ProcedureType.NonQuery, null, new FieldInfo[0], CreateNonQueryMethod());
            },
            "The non query call must be generated successfully");
        }

        private ILGenerator CreateScalarMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IMixedOperations));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("ScalarMethod", MethodAttributes.Public, typeof(DataSet), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ILGenerator CreateNonQueryMethod()
        {
            TypeBuilder typeBuilder = GenerationUtils.GetTypeBuilder(typeof(IMixedOperations));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("NonQueryMethod", MethodAttributes.Public, typeof(int), new Type[0]);
            return methodBuilder.GetILGenerator();
        }

        private ProcedureCallGenerator procedureCallGenerator;
    }
}
