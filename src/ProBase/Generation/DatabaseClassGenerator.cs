using ProBase.Attributes;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    internal class DatabaseClassGenerator : IConcreteClassGenerator
    {
        public string AssemblyName { get; set; }

        public string ModuleName { get; set; }

        public string ClassName { get; set; }

        public DatabaseClassGenerator(IMethodGenerator methodGenerator)
        {
            this.methodGenerator = methodGenerator;
        }

        public Type GenerateClassImplementingInterface(Type interfaceType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (interfaceType.GetCustomAttribute<DatabaseInterfaceAttribute>() == null)
            {
                throw new ArgumentException("The type provided must be marked with the DatabaseInterface attribute", nameof(interfaceType));
            }

            return GenerateInternal(interfaceType);
        }

        private Type GenerateInternal(Type interfaceType)
        {
            AssemblyBuilder assemblyBuilder = CreateAssemblyBuilder(AssemblyName);
            ModuleBuilder moduleBuilder = CreateModuleBuilder(ModuleName, assemblyBuilder);

            TypeBuilder typeBuilder = CreateTypeBuilder(ClassName, moduleBuilder);
            typeBuilder.AddInterfaceImplementation(interfaceType);
            interfaceType.GetMethods().ToList().ForEach(method => BuildMethodImplementation(method, typeBuilder));

            return typeBuilder.AsType();
        }

        private void BuildMethodImplementation(MethodInfo methodInfo, TypeBuilder typeBuilder)
        {
            methodGenerator.GenerateMethod(methodInfo, typeBuilder);
        }

        private TypeBuilder CreateTypeBuilder(string typeName, ModuleBuilder moduleBuilder)
        {
            return moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);
        }

        private ModuleBuilder CreateModuleBuilder(string moduleName, AssemblyBuilder assemblyBuilder)
        {
            return assemblyBuilder.DefineDynamicModule(moduleName);
        }

        private AssemblyBuilder CreateAssemblyBuilder(string assemblyName)
        {
            return AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        }

        private IMethodGenerator methodGenerator;
    }
}
