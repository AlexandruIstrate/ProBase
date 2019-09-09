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

        public ProcedureAttribute(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }
}
