using System;
using System.Linq;
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
            }
            else
            {
                // We have a simple Task
            }
        }
    }
}
