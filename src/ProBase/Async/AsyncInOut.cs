using ProBase.Utils;
using System;

namespace ProBase.Async
{
    /// <summary>
    /// Represents a parameter that can be used as an async in or out value.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter</typeparam>
    public class AsyncInOut<TParameter>
    {
        /// <summary>
        /// Gets or sets the input value.
        /// </summary>
        public TParameter InValue { get; set; }

        /// <summary>
        /// Gets the output value.
        /// </summary>
        public TParameter OutValue
        {
            get => resultFunc.Invoke();
        }

        /// <summary>
        /// Sets the function used for computing the out value.
        /// </summary>
        public Func<TParameter> ResultFunc
        {
            set => resultFunc = Preconditions.CheckNotNull(value, nameof(ResultFunc));
        }

        private Func<TParameter> resultFunc;
    }
}
