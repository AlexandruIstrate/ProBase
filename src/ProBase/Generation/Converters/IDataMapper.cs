namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Represents an object whose properties can be mapped to another type.
    /// </summary>
    internal interface IDataMapper
    {
        /// <summary>
        /// Maps this object to an object of the given type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="obj">The object to map</param>
        /// <returns>A mapped object</returns>
        T MapToObject<T>(object obj) where T : new();

        /// <summary>
        /// Checks if this object can be mapped to the given type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to test the maping for</typeparam>
        /// /// <param name="obj">The object to check the mapping for</param>
        /// <returns>True if the type can be mapped, false otherwise</returns>
        bool CanBeRepresentedAs<T>(object obj);
    }
}