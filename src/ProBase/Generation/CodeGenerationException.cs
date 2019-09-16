using System;

namespace ProBase.Generation
{
    /// <summary>
    /// Represents an error that occurs during code generation.
    /// </summary>
    public class CodeGenerationException : Exception
    {
        public CodeGenerationException() : base() { }

        public CodeGenerationException(string message) : base(message) { }

        public CodeGenerationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
