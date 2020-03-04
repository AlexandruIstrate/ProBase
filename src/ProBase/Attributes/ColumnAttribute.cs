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

        /// <summary>
        /// Whether the name of this column is case sensitive or not.
        /// </summary>
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Specifies how a column should be serialized.
        /// </summary>
        public SerializationBehavior Serialization { get; set; } = SerializationBehavior.Serialize | SerializationBehavior.Unserialize;

        /// <summary>
        /// Initializes an instance of this class with the given column name;
        /// </summary>
        /// <param name="columnName">The column name</param>
        public ColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }

    /// <summary>
    /// Provides a set of values that specify how a column should be serialized.
    /// </summary>
    [Flags]
    public enum SerializationBehavior
    {
        /// <summary>
        /// Indicates that the property is to be ignored.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the property is to be serialized.
        /// </summary>
        Serialize = 1,

        /// <summary>
        /// Indicates that the property is to be unserialized.
        /// </summary>
        Unserialize = 2
    }
}
