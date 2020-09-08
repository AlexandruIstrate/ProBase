using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Utils
{
    /// <summary>
    /// Represents a collection of parameters.
    /// </summary>
    internal class ParameterCollection
    {
        /// <summary>
        /// The local for the collection.
        /// </summary>
        public LocalBuilder CollectionLocal { get; set; }

        /// <summary>
        /// Gets or sets the builder for the given parameter.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>The builder</returns>
        public LocalBuilder this[ParameterInfo parameter]
        {
            get => GetBuilderForParameter(parameter);
            set => SetBuilderForParameter(parameter, value);
        }

        /// <summary>
        /// Gets the builder for the given parameter.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>The builder for the given parameter</returns>
        public LocalBuilder GetBuilderForParameter(ParameterInfo parameter)
        {
            if (!parameterDictionary.TryGetValue(parameter, out LocalBuilder value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Sets the builder for the given parameter.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <param name="builder">The builder</param>
        public void SetBuilderForParameter(ParameterInfo parameter, LocalBuilder builder)
        {
            parameterDictionary[parameter] = builder;
        }

        private readonly Dictionary<ParameterInfo, LocalBuilder> parameterDictionary = new Dictionary<ParameterInfo, LocalBuilder>();
    }
}
