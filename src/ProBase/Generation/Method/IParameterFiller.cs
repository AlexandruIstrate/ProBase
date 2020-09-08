using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Provides a way of filling method parameters from procedure output parameters.
    /// </summary>
    internal interface IParameterFiller
    {
        /// <summary>
        /// Fills the given parameter with the value returned by the procedure.
        /// </summary>
        /// <param name="parameter">The method parameter</param>
        /// <param name="dbParameter">The procedure parameter local index</param>
        /// <param name="generator">The generator used for IL generation</param>
        void Fill(ParameterInfo parameter, int dbParameter, ILGenerator generator);
    }
}
