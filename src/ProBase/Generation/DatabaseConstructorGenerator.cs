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

        public ConstructorBuilder GenerateDependencyConstructor(Type[] fields, TypeBuilder typeBuilder)
        {
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, fields);
            GenerateConstructorInternal(constructorBuilder.GetILGenerator(), fields, typeBuilder.BaseType);
            return constructorBuilder;
        }

        private void GenerateConstructorInternal(ILGenerator generator, Type[] fields, Type baseType)
        {
            // Load the constructor parameters
            for (int i = 0; i < fields.Count(); i++)
            { 
                generator.Emit(OpCodes.Ldarg, i);
            }

            CallBaseConstructor(generator, baseType);

            for (int i = 0; i < fields.Count(); i++)
            {
                // Load the constructor parameter
                generator.Emit(OpCodes.Ldarg, i);

                // Load the class field
                generator.Emit(OpCodes.Ldarg, i + fields.Count());

                // Replaced the currently stored field value with the value of the constructor parameter
                generator.Emit(OpCodes.Stfld, GetType().GetField(fields[i].Name));
            }

            // Return from the constructor
            generator.Emit(OpCodes.Ret);
        }

        private void CallBaseConstructor(ILGenerator generator, Type baseType)
        {
            ConstructorInfo defaultConstructor = baseType.GetConstructor(null);

            if (defaultConstructor == null)
            {
                throw new CodeGenerationException("The base type does not have a default constructor");
            }

            generator.Emit(OpCodes.Call, defaultConstructor);
        }
    }
}
