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
                ReturnDataSet();
            }
            else
            {
                MapResults(resultType);
            }
        }

        private void ReturnDataSet()
        {

        }

        private void MapResults(Type mapType)
        {

        }
    }
}
