using ProBase.Async;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Provides a way of filling async method parameters from procedure output parameters.
    /// </summary>
    internal class AsyncParameterFiller : IParameterFiller
    {
        /// <summary>
        /// Fills the given <see cref="ProBase.Async.AsyncOut{TParameter}"/>> parameter with the value returned by the procedure.
        /// </summary>
        /// <param name="parameter">The method parameter</param>
        /// <param name="dbParameter">The procedure parameter local index</param>
        /// <param name="generator">The generator used for IL generation</param>
        public void Fill(ParameterInfo parameter, int dbParameter, ILGenerator generator)
        {
            // Create the ParameterAdapter object
            CreateAdapter(dbParameter, generator);

            // Set the value of the Task<>
            SetTaskValue(parameter, dbParameter, generator);
        }

        private static void CreateAdapter(int dbParameter, ILGenerator generator)
        {
            // Load the local DbParameter
            generator.Emit(OpCodes.Ldloc, dbParameter);

            // Create a new ParameterAdapter object
            generator.Emit(OpCodes.Newobj, GetAdapterConstructor());

            // Store the new value into the local
            generator.Emit(OpCodes.Stloc);
        }

        private void SetTaskValue(ParameterInfo parameter, int dbParameter, ILGenerator generator)
        {
            Type genericType = GetParameterGenericType(parameter);

            // Load the AsyncOut parameter
            generator.Emit(OpCodes.Ldarg, parameter.Position);

            // Load the local DbParameter
            generator.Emit(OpCodes.Ldloc, dbParameter);

            // Load the FillParameter method for the specified parameter
            generator.Emit(OpCodes.Ldftn, GetAdapterFillMethod(genericType));

            // Create a new Func<> object
            generator.Emit(OpCodes.Newobj, GetFuncConstructor(genericType));

            // Create a new task
            generator.Emit(OpCodes.Newobj, GetTaskConstructor(genericType));

            // Set the value of the ResultTask property
            generator.Emit(OpCodes.Callvirt, GetResultTaskSetMethod());
        }

        private MethodInfo GetAdapterFillMethod(Type type)
        {
            Type adapterType = typeof(ParameterAdapter);
            MethodInfo method = adapterType.GetMethod("FillParameter");
            return method.MakeGenericMethod(new[] { type });
        }

        private ConstructorInfo GetFuncConstructor(Type genericType)
        {
            Type funcType = typeof(Func<>);
            Type constructed = funcType.MakeGenericType(genericType);
            return constructed.GetConstructor(new[] { typeof(object), typeof(int) });
        }

        private Type GetParameterGenericType(ParameterInfo parameterInfo)
        {
            Type type = parameterInfo.ParameterType;

            if (!type.IsGenericType)
            {
                throw new CodeGenerationException("The provided parameter is not generic");
            }

            Type genericType = type.GetGenericArguments().First();

            return genericType;
        }

        private static ConstructorInfo GetAdapterConstructor()
        {
            return typeof(ParameterAdapter).GetConstructor(new[] { typeof(DbParameter) });
        }

        private ConstructorInfo GetTaskConstructor(Type genericType)
        {
            Type taskType = typeof(Task<>).MakeGenericType(genericType);
            Type funcType = typeof(Func<>).MakeGenericType(genericType);
            return taskType.GetConstructor(new[] { funcType });
        }

        private static MethodInfo GetResultTaskSetMethod()
        {
            return typeof(AsyncOut<>).GetProperty("ResultTask").GetSetMethod();
        }
    }
}
