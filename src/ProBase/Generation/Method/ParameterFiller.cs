using ProBase.Generation.Converters;
using ProBase.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    internal class ParameterFiller : IParameterFiller
    {
        public void Fill(ParameterInfo parameter, int dbParameter, ILGenerator generator)
        {
            // Load the method parameter
            generator.Emit(OpCodes.Ldarg, parameter.Position + 1);

            // Load the DbParameter local
            generator.Emit(OpCodes.Ldloc, dbParameter);

            // Call the get method on the Value property
            generator.Emit(OpCodes.Callvirt, ClassUtils.GetPropertyGetMethod<DbParameter>(nameof(DbParameter.Value)));

            // Call the Convert method
            generator.Emit(OpCodes.Call, GetConvertMethod(parameter.ParameterType.GetElementType()));

            // Store the value into the method parameter
            StoreValue(parameter, generator);
        }

        private static void StoreValue(ParameterInfo parameter, ILGenerator generator)
        {
            ParameterDirection direction = parameter.GetDbParameterDirection();

            if (direction == ParameterDirection.Output)
            {
                // Store the resulting value into the parameter
                generator.Emit(OpCodes.Stind_Ref);
            }
            else if (direction == ParameterDirection.InputOutput)
            {
                // Store the resulting value into the parameter address
                generator.Emit(OpCodes.Stind_I4);
            }
        }

        private MethodInfo GetConvertMethod(Type type)
        {
            //return typeof(Convert).GetMethod($"To{ type.Name }", types: new[] { typeof(object) });

            IEnumerable<MethodInfo> methods = typeof(Convert).GetMethods();

            IEnumerable<MethodInfo> namedMethods = methods.Where(m => m.Name == $"To{ type.Name }");

            IEnumerable<MethodInfo> oneParameterMethods = namedMethods.Where(m => m.GetParameters().Length == 1);

            IEnumerable<MethodInfo> methodsWithParameter = oneParameterMethods.Where(m => m.GetParameters().First().ParameterType == typeof(object));

            return methodsWithParameter.First();
        }
    }
}
