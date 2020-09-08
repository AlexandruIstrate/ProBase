using System;

namespace ProBase
{
    /// <summary>
    /// Represents errors that occur while mapping an interface to database procedures.
    /// </summary>
    public class OperationMappingException : Exception
    {
        public OperationMappingException() : base() { }

        public OperationMappingException(string message) : base(message) { }

        public OperationMappingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
