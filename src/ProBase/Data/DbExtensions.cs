using System.Data.Common;
using System.Reflection;

namespace ProBase.Data
{
    internal static class DbExtensions
    {
        public static DbProviderFactory GetProviderFactory(this DbConnection connection)
        {
            PropertyInfo factoryField = connection.GetType().GetProperty("DbProviderFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            return (DbProviderFactory)factoryField.GetValue(connection);
        }
    }
}
