namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides a way of creating <see cref="ProBase.Generation.Converters.IParameterConverter"/> objects.
    /// </summary>
    internal static class ParameterConverterFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ProBase.Generation.Converters.IParameterConverter"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ProBase.Generation.Converters.IParameterConverter"/></returns>
        public static IParameterConverter Create()
        {
            return new ParameterConverter();
        }
    }
}
