using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace ProBase.Tests
{
    [TestFixture]
    public class ClassGenerationTest
    {
        private const string AssemblyName = "TestAssembly";
        private const string ClassName = "GeneratedObject";
        private const string MethodName = "TestMethod";

        [Test]
        public void CanGenerateCode()
        {
            AssemblyName assemblyName = new AssemblyName(AssemblyName);
            AppDomain appDomain = Thread.GetDomain();

            AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(ClassName, TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.AddInterfaceImplementation(typeof(IEnumerable<string>));

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(MethodName, MethodAttributes.Public | 
                                                                               MethodAttributes.Final | 
                                                                               MethodAttributes.Virtual);
            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
        }
    }
}
