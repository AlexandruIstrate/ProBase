﻿using ProBase.Utils;
using System;

namespace ProBase.Async
{
    /// <summary>
    /// Represents a parameter that can be used as an async out value.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
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

        private Func<TParameter> resultFunc;
    }
}
