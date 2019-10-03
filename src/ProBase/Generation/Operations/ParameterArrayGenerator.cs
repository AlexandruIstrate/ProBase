using ProBase.Generation.Converters;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Operations
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

        public void Generate(ParameterInfo[] parameters, ILGenerator generator)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter databaseParameter = parameterConverter.ConvertParameter(parameters[i], value: null);

                // Create the parameter
                CreateParameter(localIndex: i, generator);

                // Set the direction for this parameter
                SetParameterDirection(variableIndex: i, databaseParameter.Direction, generator);

                // Set the parameter value
                SetParameterValue(variableIndex: i, valueIndex: i + 1, generator);
            }

            CreateArray(typeof(DbParameter), parameters.Length, generator);
        }

        private void CreateParameter(int localIndex, ILGenerator generator)
        {
            // Load this object to the stack
            generator.Emit(OpCodes.Ldarg, 0);

            // Loads the field used for creating the parameter onto the stack
            generator.Emit(OpCodes.Ldfld, GetParameterFactoryType());

            // Calls the creation method on the field
            generator.Emit(OpCodes.Callvirt, GetParameterCreationMethod());

            // Unload this object from the stack
            generator.Emit(OpCodes.Stloc, localIndex);
        }

        private void SetParameterDirection(int variableIndex, ParameterDirection parameterDirection, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, variableIndex);

            // Load the parameter direction
            generator.Emit(OpCodes.Ldc_I4, (int)parameterDirection);

            // Call the setter for the direction
            generator.Emit(OpCodes.Callvirt, GetSetMethod(typeof(DbParameter), nameof(DbParameter.Direction)));
        }

        private void SetParameterValue(int variableIndex, int valueIndex, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, variableIndex);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, GetSetMethod(typeof(DbParameter), nameof(DbParameter.Value)));
        }

        private void CreateArray(Type type, int length, ILGenerator generator)
        {
            // Load the array size
            generator.Emit(OpCodes.Ldc_I4, length);

            // Create the array
            generator.Emit(OpCodes.Newarr, type);

            for (int i = 0; i < length; i++)
            {
                // Duplicate the local variable
                generator.Emit(OpCodes.Dup);

                // Load the array index
                generator.Emit(OpCodes.Ldc_I4, i);

                // Load the current local variable
                generator.Emit(OpCodes.Ldloc, i);

                // Replace the array argument with the local argument
                generator.Emit(OpCodes.Stelem_Ref);
            }
        }

        private Type GetParameterFactoryType()
        {
            return typeof(DbProviderFactory);
        }

        private MethodInfo GetParameterCreationMethod()
        {
            return typeof(DbProviderFactory).GetMethod(nameof(DbProviderFactory.CreateParameter));
        }

        private MethodInfo GetSetMethod(Type type, string propertyName)
        {
            return type.GetProperty(propertyName).GetSetMethod();
        }

        private readonly IParameterConverter parameterConverter;
    }
}
