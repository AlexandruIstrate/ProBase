using NUnit.Framework;
using ProBase.Data;
using ProBase.Generation;
using ProBase.Utils;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation
{
    [TestFixture]
    public class MethodGenerationTest
    {
        [Test]
        public void CanCreateMethodGenerator()
        {
            IMethodGenerator methodGenerator = null;

            Assert.DoesNotThrow(() =>
            {
                methodGenerator = new DatabaseMethodGenerator();
            },
            "The creation of the method generator must be successful");
        }

        [Test]
        public void CanGenerateMethodImplementation()
        {
            IMethodGenerator methodGenerator = new DatabaseMethodGenerator();

            foreach (MethodInfo method in typeof(IGenerationTestInterface).GetMethods())
            {
                MethodBuilder methodBuilder = null;

                Assert.DoesNotThrow(() =>
                {
                    TypeBuilder typeBuilder = GetTypeBuilder(typeof(IGenerationTestInterface));
                    methodBuilder = methodGenerator.GenerateMethod(method, new FieldInfo[] { GetDatabaseMapperField(typeBuilder) }, typeBuilder);
                },
                "The method generation must be successful");

                Assert.NotNull(methodBuilder, "The method generator must return a non-null value");
            }
        }

        private TypeBuilder GetTypeBuilder(Type interfaceType)
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(TypeNames.GetAssemblyName()), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(TypeNames.GetModuleName());

            TypeBuilder typeBuilder = moduleBuilder.DefineType(TypeNames.GenerateUniqueTypeName("TestType"), TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.AddInterfaceImplementation(interfaceType);

            return typeBuilder;
        }

        private FieldInfo GetDatabaseMapperField(TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineField("procedureMapper", typeof(IProcedureMapper), FieldAttributes.Private | FieldAttributes.InitOnly);
        }
    }
}
