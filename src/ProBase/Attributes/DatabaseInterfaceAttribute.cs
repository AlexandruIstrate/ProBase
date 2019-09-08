using System;

namespace ProBase.Attributes
{
    /// <summary>
    /// Specifies that a certain interface can be used to map database procedures. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class DatabaseInterfaceAttribute : Attribute
    {
    }
}
