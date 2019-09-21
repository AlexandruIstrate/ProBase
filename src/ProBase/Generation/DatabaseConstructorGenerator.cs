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
        public ConstructorBuilder GenerateDefaultConstructor(IDictionary<Type, ValueType> fieldValues, TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
        }

        public ConstructorBuilder GenerateDependencyConstructor(FieldInfo[] fields, TypeBuilder typeBuilder)
        {
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, GetFieldTypes(fields));
            GenerateConstructorInternal(constructorBuilder.GetILGenerator(), fields, typeBuilder.BaseType);
            return constructorBuilder;
        }

        private void GenerateConstructorInternal(ILGenerator generator, FieldInfo[] fields, Type baseType)
        {
            // Load the constructor parameters
            for (int i = 0; i < fields.Count(); i++)
            { 
                generator.Emit(OpCodes.Ldarg, i);
            }

            // Call the default base constructor of this class first to ensure the type is constructed properly
            CallDefaultBaseConstructor(generator, baseType);

            for (int i = 0; i < fields.Count(); i++)
            {
                // Load the constructor parameter
                generator.Emit(OpCodes.Ldarg, i);

                // Load the class field
                generator.Emit(OpCodes.Ldarg, i + fields.Count());

                // Replace the currently stored field value with the value of the constructor parameter
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
            ConstructorInfo defaultConstructor = baseType.GetConstructor(new Type[] { });

            if (defaultConstructor == null)
            {
                throw new CodeGenerationException("The base type does not have a default constructor");
            }

            generator.Emit(OpCodes.Call, defaultConstructor);
        }
    }
}
