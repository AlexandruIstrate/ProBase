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
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        public DbParameter Parameter { get; set; }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        public ParameterAdapter(DbParameter parameter)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// Fills the parameter with the converted value.
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <returns>The converted value</returns>
        public T FillParameter<T>()
        {
            MethodInfo convertMethod = ClassUtils.GetConvertMethod(typeof(T));
            return (T)convertMethod.Invoke(null, new[] { Parameter.Value });
        }
    }
}
