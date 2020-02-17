using ProBase.Utils;
using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Fills Task results from parameter values.
    /// </summary>
    internal class ParameterAdapter
    {
        public DbParameter Parameter { get; set; }

        public ParameterAdapter(DbParameter parameter)
        {
            Parameter = parameter;
        }

        public T FillParameter<T>()
        {
            MethodInfo convertMethod = ClassUtils.GetConvertMethod(typeof(T));
            return (T)convertMethod.Invoke(null, new[] { Parameter.Value });
        }
    }
}
