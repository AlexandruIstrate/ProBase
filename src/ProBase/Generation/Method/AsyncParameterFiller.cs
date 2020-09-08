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
            LocalBuilder adapter = CreateAdapter(dbParameter, generator);

            // Set the value of the Task<>
            SetFuncValue(parameter, adapter, generator);
        }

        private LocalBuilder CreateAdapter(int dbParameter, ILGenerator generator)
        {
            // Load the local DbParameter
            generator.Emit(OpCodes.Ldloc, dbParameter);

            // Declare the ParameterAdapter
            LocalBuilder adapter = generator.DeclareLocal(typeof(ParameterAdapter));

            // Create a new ParameterAdapter object
            generator.Emit(OpCodes.Newobj, GetAdapterConstructor());

            // Store the new value into the local
            generator.Emit(OpCodes.Stloc, adapter.LocalIndex);

            return adapter;
        }

        private void SetFuncValue(ParameterInfo parameter, LocalBuilder adapter, ILGenerator generator)
        {
            Type genericType = GetParameterGenericType(parameter);

            // Load the AsyncOut parameter
            generator.Emit(OpCodes.Ldarg, parameter.Position + 1);

            // Load the local ParameterAdapter
            generator.Emit(OpCodes.Ldloc, adapter);

            // Load the FillParameter method for the specified parameter
            generator.Emit(OpCodes.Ldftn, GetAdapterFillMethod(genericType));

            // Create a new Func<> object
            generator.Emit(OpCodes.Newobj, GetFuncConstructor(genericType));

            // Set the value of the ResultValue property
            generator.Emit(OpCodes.Callvirt, GetResultFuncSetMethod(parameter.ParameterType.GetGenericArguments().First()));
        }

        private MethodInfo GetAdapterFillMethod(Type type)
        {
            Type adapterType = typeof(ParameterAdapter);
            MethodInfo method = adapterType.GetMethod(FillMethod);
            return method.MakeGenericMethod(new[] { type });
        }

        private ConstructorInfo GetFuncConstructor(Type genericType)
        {
            Type funcType = typeof(Func<>);
            Type constructed = funcType.MakeGenericType(genericType);
            return constructed.GetConstructor(new[] { typeof(object), typeof(IntPtr) });
        }

        private Type GetParameterGenericType(ParameterInfo parameterInfo)
        {
            Type type = parameterInfo.ParameterType;

            if (!type.IsGenericType)
            {
                throw new CodeGenerationException("The provided parameter is not generic");
            }

            return type.GetGenericArguments().First();
        }

        private ConstructorInfo GetAdapterConstructor()
        {
            return typeof(ParameterAdapter).GetConstructor(new[] { typeof(DbParameter) });
        }

        private ConstructorInfo GetTaskConstructor(Type genericType)
        {
            Type taskType = typeof(Task<>).MakeGenericType(genericType);
            Type funcType = typeof(Func<>).MakeGenericType(genericType);
            return taskType.GetConstructor(new[] { funcType });
        }

        private MethodInfo GetResultFuncSetMethod(Type resultType)
        {
            return typeof(AsyncOut<>).MakeGenericType(resultType).GetProperty(ResultFunc).GetSetMethod();
        }

        private const string FillMethod = nameof(ParameterAdapter.FillParameter);
        private const string ResultFunc = nameof(AsyncOut<object>.ResultFunc);
    }
}
