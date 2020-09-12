using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Specifies how a method maps to a database procedure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ProcedureAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the procedure this method maps to.
        /// </summary>
        public string ProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the name of the database schema to use.
        /// </summary>
        public string DatabaseSchema { get; set; }

        /// <summary>
        /// Gets or sets the type of procedure.
        /// </summary>
        public ProcedureType ProcedureType { get; set; }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        public ProcedureAttribute(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }

    /// <summary>
    /// Indicates the type of a procedure.
    /// </summary>
    public enum ProcedureType
    {
        /// <summary>
        /// The type of this procedure will be automatically deduced.
        /// </summary>
        Automatic,

        /// <summary>
        /// This procedure returns a value.
        /// </summary>
        Scalar,

        /// <summary>
        /// This procedure does not have a result but returns the number of rows affected.
        /// </summary>
        NonQuery
    }
}
