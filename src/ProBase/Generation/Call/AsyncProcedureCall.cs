using ProBase.Data;
using ProBase.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Call
{
    internal class AsyncProcedureCall : IProcedureCall
    {
        public void Call(string procedureName, Type resultType, ILGenerator generator)
        {
            if (resultType.IsGenericType)
            {
                // We have a Task<T>

                // Get the only generic parameter of this Task
                Type taskType = resultType.GetGenericArguments().First();

                // Call the mapper method
                generator.Emit(OpCodes.Callvirt, GetResultTaskMethod(taskType));
            }
            else
            {
                // We have a simple Task

                // In this case, execute an async non-query. The result type will be of type
                // Task<int> but we can use it as a plain Task since Task<T> inherrits from Task.
                generator.Emit(OpCodes.Callvirt, GetNonQuerryMethod());
            }
        }

        private MethodInfo GetResultTaskMethod(Type returnType)
        {
            // For an int, we have an non-query call
            if (returnType == typeof(int))
            {
                return GetNonQuerryMethod();
            }

            // For a DataSet, we have an unmapped scalar call
            if (returnType == typeof(DataSet))
            {
                return ClassUtils.GetMethod<IProcedureMapper>(ScalarProcedure);
            }

            // If we have any other type, then it must be mapped
            return GetMapMethod(returnType);
        }

        private MethodInfo GetNonQuerryMethod()
        {
            return ClassUtils.GetMethod<IProcedureMapper>(NonQueryProcedure);
        }

        private MethodInfo GetMapMethod(Type mapType)
        {
            if (mapType.IsGenericTypeDefinition(typeof(IEnumerable<>)))
            {
                return ClassUtils.GetMethod<IProcedureMapper>(MappedEnumerableProcedure).MakeGenericMethod(mapType.GetGenericArguments().First());
            }

            return ClassUtils.GetMethod<IProcedureMapper>(MappedProcedure).MakeGenericMethod(mapType);
        }

        private const string ScalarProcedure = nameof(IProcedureMapper.ExecuteScalarProcedureAsync);
        private const string NonQueryProcedure = nameof(IProcedureMapper.ExecuteNonQueryProcedureAsync);

        private const string MappedProcedure = nameof(IProcedureMapper.ExecuteMappedProcedureAsync);
        private const string MappedEnumerableProcedure = nameof(IProcedureMapper.ExecuteEnumerableMappedProcedureAsync);
    }
}
