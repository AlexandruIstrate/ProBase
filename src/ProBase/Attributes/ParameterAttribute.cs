using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Specifies how a method parameter maps to a procedure parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name the procedure parameter this method parameter maps to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the maximum size, in bytes, of the data within the column.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Creates a new instance of this class with the supplied name.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        public ParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
