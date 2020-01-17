using ProBase.Data;
using System;
using System.Data;
using System.Reflection.Emit;

namespace ProBase.Generation.Call
{
    /// <summary>
    /// Generates calls for scalar procedures.
    /// </summary>
    internal class ScalarProcedureCall : IProcedureCall
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
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetMethod<IProcedureMapper>(ScalarProcedureMethodName));
        }

        private void MapResults(Type mapType, ILGenerator generator)
        {
            // Call the procedure mapping the type
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetMethod<IProcedureMapper>(MappedProcedureMethodName).MakeGenericMethod(mapType));
        }

        private const string ScalarProcedureMethodName = nameof(IProcedureMapper.ExecuteScalarProcedure);
        private const string MappedProcedureMethodName = nameof(IProcedureMapper.ExecuteMappedProcedure);
    }
}
