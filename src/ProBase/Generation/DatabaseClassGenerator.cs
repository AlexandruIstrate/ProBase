using ProBase.Attributes;
using ProBase.Data;
using ProBase.Utils;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Generates a class for accessing a database.
    /// </summary>
    internal class DatabaseClassGenerator : IConcreteClassGenerator
    {
        /// <summary>
        /// Gets the name of the assembly the class will generate into.
        /// </summary>
        public string AssemblyName { get; } = TypeNames.GetAssemblyName();

        /// <summary>
        /// Gets the name of the module the generated class will use.
        /// </summary>
        public string ModuleName { get; } = TypeNames.GetModuleName();

        /// <summary>
        /// Gets the name the generated class will use.
        /// </summary>
        public string ClassName { get; } = TypeNames.GenerateUniqueTypeName("GeneratedDatabaseInterface");

        /// <summary>
        /// Generates the database access class using the given components for code generation.
        /// </summary>
        /// <param name="fieldGenerator">A field generator to use for generating the field used to store the object that accesses the database</param>
        /// <param name="constructorGenerator">A constructor generator used for generating an initializing constructor</param>
        /// <param name="methodGenerator">A method generator used for generating the method implementations of the class</param>
        public DatabaseClassGenerator(IClassFieldGenerator fieldGenerator, IConstructorGenerator constructorGenerator, IMethodGenerator methodGenerator)
        {
            this.fieldGenerator = Preconditions.CheckNotNull(fieldGenerator, nameof(fieldGenerator));
            this.constructorGenerator = Preconditions.CheckNotNull(constructorGenerator, nameof(constructorGenerator));
            this.methodGenerator = Preconditions.CheckNotNull(methodGenerator, nameof(methodGenerator));
        }

        public Type GenerateClassImplementingInterface(Type interfaceType)
        {
            Preconditions.CheckNotNull(interfaceType, nameof(interfaceType));

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
            FieldBuilder fieldBuilder = fieldGenerator.GenerateField("procedureMapper", typeof(IProcedureMapper), typeBuilder);

            // Generate a constructor that initializes the IDatabaseMapper field
            constructorGenerator.GenerateDependencyConstructor(new FieldInfo[] { fieldBuilder }, typeBuilder);

            // Generate implementations for all of the methods defined by the interface
            interfaceType.GetMethods().ToList().ForEach(method => BuildMethodImplementation(method, new FieldInfo[] { fieldBuilder }, typeBuilder));

            // Generate the meta-type from the builder
            return typeBuilder.CreateTypeInfo();
        }

        private void BuildMethodImplementation(MethodInfo methodInfo, FieldInfo[] classFields, TypeBuilder typeBuilder)
        {
            methodGenerator.GenerateMethod(methodInfo, classFields, typeBuilder);
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
