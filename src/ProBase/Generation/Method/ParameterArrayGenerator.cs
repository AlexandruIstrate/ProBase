using ProBase.Generation.Converters;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Generates an array of parameters used for calling database procedures.
    /// </summary>
    internal class ParameterArrayGenerator : IArrayGenerator
    {
        /// <summary>
        /// Creates an instance of this class using the given <see cref="ProBase.Generation.Converters.IParameterConverter"/> for converting the method parameters to database parameters.
        /// </summary>
        /// <param name="parameterConverter">The converter used for converting method parameters</param>
        public ParameterArrayGenerator(IParameterConverter parameterConverter)
        {
            this.parameterConverter = parameterConverter;
        }

        public LocalBuilder Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            // Get the provider factory field
            FieldInfo providerFactory = GeneratedClass.GetField<DbProviderFactory>(fields, GenerationConstants.ProviderFactoryFieldName);

            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter databaseParameter = parameterConverter.ConvertParameter(parameters[i], value: null);

                // Create the parameter
                LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

                // Set the name for this parameter
                SetParameterName(parameterBuilder, databaseParameter.ParameterName, generator);

                // Set the direction for this parameter
                SetParameterDirection(parameterBuilder, databaseParameter.Direction, generator);

                // Set the parameter value
                SetParameterValue(parameterBuilder, valueIndex: i + 1, generator);
            }

            // Create the array
            return CreateArray(typeof(DbParameter[]), parameters.Length, generator);
        }

        private LocalBuilder CreateParameter(FieldInfo providerFactory, ILGenerator generator)
        {
            // Declare the variable
            LocalBuilder localBuilder = generator.DeclareLocal(typeof(DbParameter));

            // Load this object to the stack
            generator.Emit(OpCodes.Ldarg_0);

            // Loads the field used for creating the parameter onto the stack
            generator.Emit(OpCodes.Ldfld, providerFactory);

            // Calls the creation method on the field
            generator.Emit(OpCodes.Callvirt, GetParameterCreationMethod());

            // Unload this object from the stack
            generator.Emit(OpCodes.Stloc, localBuilder);

            return localBuilder;
        }

        private void SetParameterName(LocalBuilder parameterBuilder, string name, ILGenerator generator)
        {
            // Load the variable at the given index
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter name
            generator.Emit(OpCodes.Ldstr, name);

            // Call the setter for the name
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetPropertySetMethod<DbParameter>(nameof(DbParameter.ParameterName)));
        }

        private void SetParameterDirection(LocalBuilder parameterBuilder, ParameterDirection parameterDirection, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter direction
            generator.Emit(OpCodes.Ldc_I4, (int)parameterDirection);

            // Call the setter for the direction
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Direction)));
        }

        private void SetParameterValue(LocalBuilder parameterBuilder, int valueIndex, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // If the value is of a primitive type, box it
            if (parameterBuilder.LocalType.IsPrimitive)
            {
                // Box the primitive value
                generator.Emit(OpCodes.Box, parameterBuilder.LocalType);
            }

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Value)));
        }

        private LocalBuilder CreateArray(Type type, int length, ILGenerator generator)
        {
            LocalBuilder localBuilder = generator.DeclareLocal(type);

            // Load the array size onto the evaluation stack
            generator.Emit(OpCodes.Ldc_I4, length);

            // Create the array
            generator.Emit(OpCodes.Newobj, GetArrayConstructor(type));

            // Store the newly created value into the local
            generator.Emit(OpCodes.Stloc, localBuilder);

            for (int i = 0; i < length; i++)
            {
                // Load the local array
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // Load the array index onto the stack
                generator.Emit(OpCodes.Ldc_I4, i);

                // Loads the local parameter at index
                generator.Emit(OpCodes.Ldloc, i);

                // Set the array element at index to the current local value
                generator.Emit(OpCodes.Stelem_Ref);
            }

            return localBuilder;
        }

        private MethodInfo GetParameterCreationMethod()
        {
            return typeof(DbProviderFactory).GetMethod(nameof(DbProviderFactory.CreateParameter));
        }

        private ConstructorInfo GetArrayConstructor(Type arrayType) => arrayType.GetConstructor(new[] { typeof(int) });

        private readonly IParameterConverter parameterConverter;
    }
}
