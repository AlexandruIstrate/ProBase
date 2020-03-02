using ProBase.Generation.Converters;
using ProBase.Utils;
using System;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    internal class MappedParameterArrayGenerator : ParameterArrayGenerator
    {
        public MappedParameterArrayGenerator(IParameterConverter<DbParameter[]> parameterConverter) : base(null)
        {
            this.parameterConverter = parameterConverter;
        }

        public override LocalBuilder Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            // Get the provider factory field
            FieldInfo providerFactory = ClassUtils.GetField<DbProviderFactory>(fields, GenerationConstants.ProviderFactoryFieldName);

            int parameterCount = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter[] mappedParameters = parameterConverter.ConvertParameter(parameters[i], value: null);

                foreach (DbParameter dbParameter in mappedParameters)
                {
                    // Create the parameter
                    LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

                    // Set the name for this parameter
                    SetParameterName(parameterBuilder, dbParameter.SourceColumn, generator);

                    // Set the direction for this parameter
                    SetParameterDirection(parameterBuilder, dbParameter.Direction, generator);

                    // Set the parameter value
                    SetParameterValue(parameterBuilder, valueIndex: i + 1, parameters[i].ParameterType, dbParameter.ParameterName, generator);
                }

                parameterCount += mappedParameters.Length;
            }

            // Create the array
            return CreateArray(typeof(DbParameter[]), parameterCount, generator);
        }

        private void SetParameterValue(LocalBuilder parameterBuilder, int valueIndex, Type type, string propertyName, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // Get the property
            PropertyInfo prop = type.GetProperty(propertyName);

            // Call the get method of the property
            generator.Emit(OpCodes.Callvirt, prop.GetGetMethod());

            // If the value is of a primitive type, box it
            if (prop.PropertyType.IsPrimitive)
            {
                // Box the primitive value
                generator.Emit(OpCodes.Box, prop.PropertyType);
            }

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Value)));
        }

        private readonly IParameterConverter<DbParameter[]> parameterConverter;
    }
}
