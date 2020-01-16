using ProBase.Data;
using System;
using System.Data;
using System.Reflection;
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
        /// <param name="parameterArray">The local procedure parameters</param>
        /// <param name="fields">The generated class fields</param>
        /// <param name="generator">The generator to be used for code generation</param>
        public void Call(string procedureName, Type resultType, LocalBuilder parameterArray, FieldInfo[] fields, ILGenerator generator)
        {
            // Load this object
            generator.Emit(OpCodes.Ldarg_0);

            // Load the IProcedureMapper field
            generator.Emit(OpCodes.Ldfld, GeneratedClass.GetField<IProcedureMapper>(fields, GenerationConstants.ProcedureMapperFieldName));

            // Load the procedure name
            generator.Emit(OpCodes.Ldstr, procedureName);

            // Load the parameter array
            generator.Emit(OpCodes.Ldloc, parameterArray);

            if (resultType == typeof(DataSet))
            {
                ReturnDataSet(generator);
            }
            else
            {
                MapResults(resultType, generator);
            }

            // Return from the method
            generator.Emit(OpCodes.Ret);
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
