using System;

namespace ProBase
{
    /// <summary>
    /// Represents errors that occur while mapping an interface to database procedures.
    /// </summary>
    public class OperationMappingException : Exception
    {
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public OperationMappingException() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of this class with the supplied message.
        /// </summary>
        /// <param name="message">The error message</param>
        public OperationMappingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of this class with the supplied message and inner exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public OperationMappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
