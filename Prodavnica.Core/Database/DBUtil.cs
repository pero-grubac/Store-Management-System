using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Prodavnica.Database
{
    public static class DBUtil
    {
        // Environment variable that overrides Util/appsettings.json, e.g. in CI or a container.
        public const string ConnectionStringVariable = "STORE_DB_CONNECTION";

        private static string? _connectionString;

        public static string ConnectionString
        {
            get => _connectionString ??= LoadConnectionString();
            set => _connectionString = value;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        private static string LoadConnectionString()
        {
            string? fromEnvironment = Environment.GetEnvironmentVariable(ConnectionStringVariable);
            if (!string.IsNullOrWhiteSpace(fromEnvironment))
            {
                return fromEnvironment;
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("Util/appsettings.json", optional: true)
                .Build();

            return config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    $"Connection string not found. Create Util/appsettings.json or set {ConnectionStringVariable}.");
        }
    }
}
