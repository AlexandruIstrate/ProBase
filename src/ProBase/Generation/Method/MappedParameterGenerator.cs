using ProBase.Attributes;
using ProBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Generates parameters based on mapped, user defined types.
    /// </summary>
    internal class MappedParameterGenerator : ParameterGenerator
    {
        public override LocalBuilder[] Generate(ParameterInfo parameter, FieldInfo providerFactory, ILGenerator generator)
        {
            List<LocalBuilder> localBuilders = new List<LocalBuilder>();

            foreach (PropertyInfo property in GetProperties(parameter))
            {
                // Create the local parameter
                LocalBuilder parameterBuilder = CreateParameterInternal(property, providerFactory, parameter.Position + 1, parameter.ParameterType, generator);

                // Only consider this parameter if it's not null
                if (parameterBuilder != null)
                {
                    // Add the parameter to the locals
                    localBuilders.Add(parameterBuilder);
                }
            }

            // Return the array
            return localBuilders.ToArray();
        }

        private LocalBuilder CreateParameterInternal(PropertyInfo property, FieldInfo providerFactory, int parameterIndex, Type parameterType, ILGenerator generator)
        {
            // Get the parameter attribute
            ColumnAttribute propertyAttribute = property.GetCustomAttribute<ColumnAttribute>();

            // If the property is not set to be serialized, skip it
            if (!propertyAttribute.Serialization.HasFlag(SerializationBehavior.Serialize))
            {
                return null;
            }

            // Create the local parameter
            LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

            // Set the name of the parameter
            SetParameterName(parameterBuilder, propertyAttribute?.ColumnName ?? property.Name, generator);

            // Set the value of the parameter
            SetParameterValue(parameterBuilder, parameterIndex, parameterType, property.Name, generator);

            return parameterBuilder;
        }

        protected virtual void SetParameterValue(LocalBuilder parameterBuilder, int valueIndex, Type type, string propertyName, ILGenerator generator)
        {
            // Get the property
            PropertyInfo property = type.GetProperty(propertyName);

            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // Call the get method
            generator.Emit(OpCodes.Callvirt, property.GetGetMethod());

            // If the value is of a primitive type, box it
            if (property.PropertyType.IsPrimitive)
            {
                // Box the primitive value
                generator.Emit(OpCodes.Box, property.PropertyType);
            }

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Value)));
        }

        private IEnumerable<PropertyInfo> GetProperties(ParameterInfo parameter)
        {
            if (!parameter.GetType().IsUserDefined())
            {
                throw new CodeGenerationException("The type provided cannot be broken up into properties");
            }

            return parameter.ParameterType.GetProperties();
        }
    }
}
