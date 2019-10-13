namespace ProBase
{
    /// <summary>
    /// Represents and object that manages the creation of objects that run operations on databases.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Generates an instance of a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance implementing the passed in interface type</returns>
        T GenerateObject<T>();
    }
}