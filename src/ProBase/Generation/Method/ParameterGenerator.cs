using ProBase.Async;
using ProBase.Attributes;
using ProBase.Generation.Converters;
using ProBase.Utils;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Generates a parameter based on CLR typed method parameters.
    /// </summary>
    internal class ParameterGenerator : IParameterGenerator
    {
        public virtual LocalBuilder[] Generate(ParameterInfo parameter, FieldInfo providerFactory, ILGenerator generator)
        {
            // Create the local parameter
            LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

            // Get the parameter attribute
            ParameterAttribute parameterAttribute = parameter.GetCustomAttribute<ParameterAttribute>();

            // Set the name of the parameter
            SetParameterName(parameterBuilder, parameterAttribute?.Name ?? parameter.Name, generator);

            // Set the direction of the parameter
            SetParameterDirection(parameterBuilder, parameter.GetDbParameterDirection(), generator);

            // Set the ADO.NET type for this parameter
            if (parameter.ParameterType.IsGenericTypeDefinition(typeof(AsyncOut<>)))
            {
                // If the parameter is AsyncOut<>, use the base type for deducing the real type
                SetParameterType(parameterBuilder, TypeUtils.ConvertTypeToDbType(parameter.ParameterType.GetGenericArguments().First()), generator);
            }
            else
            {
                // If the parameter is normal, use the extension method to get its type
                SetParameterType(parameterBuilder, parameter.GetDbType(), generator);
            }

            // Don't set a value for out parameters
            if (parameter.GetDbParameterDirection() != ParameterDirection.Output)
            {
                // Set the value of the parameter
                SetParameterValue(parameterBuilder, parameter.Position + 1, parameter.ParameterType, generator);
            }

            // Set the size of the parameter
            SetParameterSize(parameterBuilder, parameter, generator);

            // Create an array and return it
            return new LocalBuilder[] { parameterBuilder };
        }

        protected virtual LocalBuilder CreateParameter(FieldInfo providerFactory, ILGenerator generator)
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

        protected virtual void SetParameterName(LocalBuilder parameterBuilder, string name, ILGenerator generator)
        {
            // Load the variable at the given index
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter name
            generator.Emit(OpCodes.Ldstr, name);

            // Call the setter for the name
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.ParameterName)));
        }

        protected virtual void SetParameterDirection(LocalBuilder parameterBuilder, ParameterDirection parameterDirection, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter direction
            generator.Emit(OpCodes.Ldc_I4, (int)parameterDirection);

            // Call the setter for the direction
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Direction)));
        }

        private void SetParameterType(LocalBuilder parameterBuilder, DbType dbType, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter type
            generator.Emit(OpCodes.Ldc_I4, (int)dbType);

            // Call the setter for the type
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.DbType)));
        }

        protected virtual void SetParameterValue(LocalBuilder parameterBuilder, int valueIndex, Type type, ILGenerator generator)
        {
            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the argument that this parameter gets its value from
            generator.Emit(OpCodes.Ldarg, valueIndex);

            // If the value is of a value type, box it
            if (type.IsValueType)
            {
                // Box the primitive value
                generator.Emit(OpCodes.Box, type);
            }

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Value)));
        }

        protected virtual void SetParameterSize(LocalBuilder parameterBuilder, ParameterInfo parameterInfo, ILGenerator generator)
        {
            ParameterAttribute attribute = parameterInfo.GetCustomAttribute<ParameterAttribute>();

            // If we don't have a ParameterAttribute, then the size is the default
            if (attribute == null)
            {
                return;
            }

            // If the size is not set then use the default
            if (attribute.Size == 0)
            {
                return;
            }

            // Load the local variable associated with this parameter
            generator.Emit(OpCodes.Ldloc, parameterBuilder);

            // Load the parameter direction
            generator.Emit(OpCodes.Ldc_I4, attribute.Size);

            // Call the set method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertySetMethod<DbParameter>(nameof(DbParameter.Size)));
        }

        private MethodInfo GetParameterCreationMethod()
        {
            return ClassUtils.GetMethod<DbProviderFactory>(nameof(DbProviderFactory.CreateParameter));
        }
    }
}
