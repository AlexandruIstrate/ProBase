using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Provides information about how database columns should be mapped to class properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// The name of the column this property maps to.
        /// </summary>
        public string ColumnName { get; set; }
    }
}
