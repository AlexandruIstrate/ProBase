﻿using System;

namespace ProBase.Generation
{
    /// <summary>
    /// Represents an error that occurs during code generation.
    /// </summary>
    public class CodeGenerationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProBase.Generation.CodeGenerationException"/> class.
        /// </summary>
        public CodeGenerationException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProBase.Generation.CodeGenerationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public CodeGenerationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProBase.Generation.CodeGenerationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException"> The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CodeGenerationException(string message, Exception innerException) : base(message, innerException) { }
    }
}