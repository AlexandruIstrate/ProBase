using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Specifies how a method parameter maps to a procedure parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name the procedure parameter this method parameter maps to
        /// </summary>
        public string ParameterName { get; set; }

        public ParameterAttribute(string procedureParameterName)
        {
            ParameterName = procedureParameterName;
        }
    }
}
