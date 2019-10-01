using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Generates a constructor for a database access class.
    /// </summary>
    internal class DatabaseConstructorGenerator : IConstructorGenerator
    {
        /// <summary>
        /// Generates a default constructor for the database access class.
        /// </summary>
        /// <param name="fieldValues">The types of the parameters and their values</param>
        /// <param name="typeBuilder">A type builder used for creating the constructor</param>
        /// <returns>A builder representing the constructor</returns>
        public ConstructorBuilder GenerateDefaultConstructor(IDictionary<Type, ValueType> fieldValues, TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
        }

        /// <summary>
        /// Generates a dependency-inverted constructor for the database access class.
        /// </summary>
        /// <param name="fields">The fields this constructor initializes</param>
        /// <param name="typeBuilder">A type builder used for creating the constructor</param>
        /// <returns>A builder representing the constructor</returns>
        public ConstructorBuilder GenerateDependencyConstructor(FieldInfo[] fields, TypeBuilder typeBuilder)
        {
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, GetFieldTypes(fields));
            GenerateConstructorInternal(constructorBuilder.GetILGenerator(), fields, typeBuilder.BaseType);
            return constructorBuilder;
        }

        private void GenerateConstructorInternal(ILGenerator generator, FieldInfo[] fields, Type baseType)
        {
            // Call the default base constructor of this class first to ensure the type is constructed properly
            CallDefaultBaseConstructor(generator, baseType);

            for (int i = 0; i < fields.Count(); i++)
            {
                // Load the reference to this object
                generator.Emit(OpCodes.Ldarg, 0);

                // Load the current constructor parameter
                generator.Emit(OpCodes.Ldarg, i + 1);

                // Replaced the value stored in the current field with the value of the parameter
                generator.Emit(OpCodes.Stfld, fields[i]);
            }

            // Return from the constructor
            generator.Emit(OpCodes.Ret);
        }

        private Type[] GetFieldTypes(FieldInfo[] fields)
        {
            return fields.ToList().Select(field => field.FieldType).ToArray();
        }

        private void CallDefaultBaseConstructor(ILGenerator generator, Type baseType)
        {
            // Load the reference to this object
            generator.Emit(OpCodes.Ldarg, 0);

            ConstructorInfo defaultConstructor = baseType.GetConstructor(new Type[] { });

            if (defaultConstructor == null)
            {
                throw new CodeGenerationException("The base type does not have a default constructor");
            }

            // Call the default base constructor
            generator.Emit(OpCodes.Call, defaultConstructor);
        }
    }
}
