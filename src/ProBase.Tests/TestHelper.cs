using Microsoft.Extensions.Configuration;

namespace ProBase.Tests
{
    public class TestHelper
    {
        private const string SettingsFile = "appsettings.json";
        private const string DevelopmentSettingsFile = "appsettings.Develop.json";

        private const string UserSecretsId = "34072567-7db0-4da4-8615-eca57bbc7bb8";

        public static IConfigurationRoot GetConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile(SettingsFile)
                .AddUserSecrets(UserSecretsId)
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static TestConfiguration GetApplicationConfiguration(string outputPath)
        {
            TestConfiguration configuration = new TestConfiguration();

            IConfigurationRoot configurationRoot = GetConfigurationRoot(outputPath);
            configurationRoot.GetSection("ProBase").Bind(configuration);

            return configuration;
        }
    }

    public class TestConfiguration
    {
        public string ApplicationName { get; set; }

        public string ServerAddress { get; set; }

        public string DatabaseName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
