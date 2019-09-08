namespace ProBase.Generation
{
    /// <summary>
    /// Represents operations that are applicable in a certain scope when using reflection.
    /// </summary>
    internal interface IReflectionContext
    {
        /// <summary>
        /// Gets the value of a class field, if applicable.
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="fieldName">The name of the field</param>
        /// <returns>The value of the class field</returns>
        T GetFieldValue<T>(string fieldName);

        /// <summary>
        /// Gets the value of a method parameter, if applicable.
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The value of the method parameter</returns>
        T GetParameterValue<T>(string parameterName);

        /// <summary>
        /// Gets the value of a local variable, if applicable.
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="localName">The name of the local variable</param>
        /// <returns>The value of the local variable</returns>
        T GetLocalValue<T>(string localName);

        /// <summary>
        /// Gets the value of the named shadowed or unshadowed variable.
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="name">The name of the variable to retrieve</param>
        /// <returns>The value of the variable</returns>
        T GetVariableValue<T>(string name);
    }
}
