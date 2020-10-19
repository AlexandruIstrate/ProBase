using System.Data.Common;
using System.Reflection;

namespace ProBase.Data
{
    /// <summary>
    /// Provides extension methods for working with databases.
    /// </summary>
    internal static class DbExtensions
    {
        /// <summary>
        /// Gets the <see cref="DbProviderFactory"/> from a <see cref="DbConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <returns>An instace of <see cref="DbProviderFactory"/></returns>
        /// <remarks>This is a hack since <see cref="DbConnection"/> does not expose the DbProviderFactory property directly</remarks>
        public static DbProviderFactory GetProviderFactory(this DbConnection connection)
        {
            PropertyInfo factoryField = connection.GetType().GetProperty("DbProviderFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            return (DbProviderFactory)factoryField.GetValue(connection);
        }
    }
}
