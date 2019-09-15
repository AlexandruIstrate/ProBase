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

        public DatabaseClassGenerator(IClassFieldGenerator fieldGenerator, IConstructorGenerator constructorGenerator, IMethodGenerator methodGenerator)
        {
            this.fieldGenerator = fieldGenerator;
            this.constructorGenerator = constructorGenerator;
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

            // Generate the IDatabase field that we use for accessing the database
            fieldGenerator.GenerateField(null, typeBuilder);

            // Generate a constructor that initializes the IDatabase field
            constructorGenerator.GenerateDependencyConstructor(null, typeBuilder);

            // Generate implementations for all of the methods defined by the interface
            interfaceType.GetMethods().ToList().ForEach(method => BuildMethodImplementation(method, typeBuilder));

            // Generate the meta-type from the builder
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

        private readonly IClassFieldGenerator fieldGenerator;
        private readonly IConstructorGenerator constructorGenerator;
        private readonly IMethodGenerator methodGenerator;
    }
}
