using System;

namespace ProBase.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ParameterAttribute : Attribute
    {
        public string ProcedureParameterName { get; set; }

        public ParameterAttribute(string procedureParameterName)
        {
            ProcedureParameterName = procedureParameterName;
        }
    }
}
