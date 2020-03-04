using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Provides a way of generating a local collection based on method parameters.
    /// </summary>
    internal interface ICollectionGenerator
    {
        /// <summary>
        /// Generates an array.
        /// </summary>
        /// <param name="parameters">The parameters passed into the method</param>
        /// <param name="fields">The fields of the generated class</param>
        /// <param name="generator">The generator to use for generating MSIL instructions</param>
        /// <returns>The local builder associated with the array</returns>
        ParameterCollection Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator);
    }

    internal class ParameterCollection
    {
        public LocalBuilder CollectionLocal { get; set; }

        public LocalBuilder this[ParameterInfo index]
        {
            get => GetBuilderForParameter(index);
            set => SetBuilderForParameter(index, value);
        }

        public LocalBuilder GetBuilderForParameter(ParameterInfo parameterInfo)
        {
            if (!parameterDictionary.TryGetValue(parameterInfo, out LocalBuilder value))
            {
                return null;
            }

            return value;
        }

        public void SetBuilderForParameter(ParameterInfo parameter, LocalBuilder builder)
        {
            parameterDictionary[parameter] = builder;
        }

        private readonly Dictionary<ParameterInfo, LocalBuilder> parameterDictionary = new Dictionary<ParameterInfo, LocalBuilder>();
    }
}
