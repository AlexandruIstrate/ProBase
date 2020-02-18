using ProBase.Async;
using ProBase.Utils;
using System.Reflection;

namespace ProBase.Generation.Method
{
    internal class ParameterFillerFactory
    {
        public static IParameterFiller Create(ParameterInfo parameter)
        {
            if (parameter.ParameterType.IsAsyncOut())
            {
                return new AsyncParameterFiller();
            }

            return new ParameterFiller();
        }
    }
}
