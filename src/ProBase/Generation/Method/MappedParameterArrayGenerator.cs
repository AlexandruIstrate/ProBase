using ProBase.Attributes;
using ProBase.Generation.Converters;
using ProBase.Utils;
using System;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using static ProBase.Generation.Converters.ParameterInfoConverter;

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

            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter[] mappedParameters = parameterConverter.ConvertParameter(parameters[i], value: null);

                foreach (DbParameter dbParameter in mappedParameters)
                {
                    // Create the parameter
                    LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

                    // Set the name for this parameter
                    SetParameterName(parameterBuilder, dbParameter.ParameterName, generator);

                    // Set the direction for this parameter
                    SetParameterDirection(parameterBuilder, dbParameter.Direction, generator);

                    // Set the parameter value
                    SetParameterValue(parameterBuilder, valueIndex: i + 1, (dbParameter as DbParameterInfo).Type, dbParameter.ParameterName, generator);

                    // Set the parameter size
                    SetParameterSize(parameterBuilder, parameters[i], generator);
                }
            }

            // Create the array
            return CreateArray(typeof(DbParameter[]), parameters.Length, generator);
        }

        private void SetParameterValue(LocalBuilder parameterBuilder, int valueIndex, Type type, string propertyName, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // Call the get method of the property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertyGetMethod(type, propertyName));

            // If the value is of a primitive type, box it
            if (type.IsPrimitive)
            {
                // Box the primitive value
                generator.Emit(OpCodes.Box, type);
            }

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Value)));
        }

        private readonly IParameterConverter<DbParameter[]> parameterConverter;
    }
}
