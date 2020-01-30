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
    /// <summary>
    /// Generates calls for scalar procedures.
    /// </summary>
    internal class MappedProcedureCall : IProcedureCall
    {
        /// <summary>
        /// Generates a scalar procedure call.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="resultType">The result type of the method</param>
        /// <param name="generator">The generator to be used for code generation</param>
        public void Call(string procedureName, Type resultType, ILGenerator generator)
        {
            if (resultType == typeof(DataSet))
            {
                ReturnDataSet(generator);
            }
            else
            {
                MapResults(resultType, generator);
            }
        }

        private void ReturnDataSet(ILGenerator generator)
        {
            // Call the procedure returning a DataSet
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetMethod<IProcedureMapper>(ScalarProcedureMethod));
        }

        private void MapResults(Type mapType, ILGenerator generator)
        {
            // Call the procedure mapping the type
            generator.Emit(OpCodes.Callvirt, GetMapMethod(mapType));
        }

        private MethodInfo GetMapMethod(Type mapType)
        {
            if (mapType.IsGenericTypeDefinition(typeof(IEnumerable<>)))
            {
                return GeneratedClass.GetMethod<IProcedureMapper>(MappedEnumerableProcedureMethod).MakeGenericMethod(mapType.GetGenericArguments().First());
            }

            return GeneratedClass.GetMethod<IProcedureMapper>(MappedProcedureMethod).MakeGenericMethod(mapType);
        }

        private const string ScalarProcedureMethod = nameof(IProcedureMapper.ExecuteScalarProcedure);

        private const string MappedProcedureMethod = nameof(IProcedureMapper.ExecuteMappedProcedure);
        private const string MappedEnumerableProcedureMethod = nameof(IProcedureMapper.ExecuteEnumerableMappedProcedure);
    }
}
