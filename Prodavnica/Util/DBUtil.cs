using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace OnlineKupovinaGUI
{
    public class DBUtil
    {
        private static readonly string _connectionString;

        static DBUtil()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("Util/appsettings.json", optional: false)
                .Build();

            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}