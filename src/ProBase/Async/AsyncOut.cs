using ProBase.Utils;
using System;

namespace ProBase.Async
{
    /// <summary>
    /// Represents a parameter that can be used as an async out value.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter</typeparam>
    public class AsyncOut<TParameter>
    {
        /// <summary>
        /// Gets the result value.
        /// </summary>
        public TParameter OutValue => resultFunc.Invoke();

        /// <summary>
        /// Sets the Func used for computing the result.
        /// </summary>
        public Func<TParameter> ResultFunc
        {
            set
            {
                resultFunc = Preconditions.CheckNotNull(value, nameof(ResultFunc));
            }
        }

        /// <summary>
        /// Returns a string that represents the out value of this object.
        /// </summary>
        /// <returns>A string that represents the out value</returns>
        public override string ToString()
        {
            return OutValue.ToString();
        }

        private Func<TParameter> resultFunc;
    }
}
