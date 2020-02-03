using ProBase.Generation.Converters;
using ProBase.Generation.Method;

namespace ProBase.Generation.Class
{
    /// <summary>
    /// Provides a way for creating instances of <see cref="ProBase.Generation.IConcreteClassGenerator"/>.
    /// </summary>
    internal static class ClassGeneratorFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ProBase.Generation.IConcreteClassGenerator"/>.
        /// </summary>
        /// <returns>A class generator instance</returns>
        public static IConcreteClassGenerator Create()
        {
            return new DbClassGenerator(new DbFieldGenerator(), new DbConstructorGenerator(), GetMethodGenerator());
        }

        private static IMethodGenerator GetMethodGenerator()
        {
            return new DbMethodGenerator(new ParameterArrayGenerator(new ParameterInfoConverter()), new ProcedureCallGenerator(), new ParameterFiller());
        }
    }
}
