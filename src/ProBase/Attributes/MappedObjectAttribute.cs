using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Specifies that a class can have its properties mapped to database columns.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MappedObjectAttribute : Attribute
    {
    }
}
