using ProBase.Utils;
using System.Reflection;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Aids in the creation of <see cref="IParameterFiller"/> objects.
    /// </summary>
    internal class ParameterFillerFactory
    {
        /// <summary>
        /// Creates an <see cref="IParameterFiller"/> object based on the <see cref="ParameterInfo"/> parameter passed.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>A new object</returns>
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
