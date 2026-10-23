using MySqlConnector;
using Prodavnica.Database;
using Testcontainers.MySql;

namespace Prodavnica.Tests.Database
{
    // One MySQL 8 container per test run, loaded with Model/DDL.sql (schema, stored procedures, app user and demo data).
    public sealed class MySqlFixture : IAsyncLifetime
    {
        private const string RootPassword = "test-root";

        private readonly MySqlContainer container = new MySqlBuilder("mysql:8.0")
            .WithUsername("root")
            .WithPassword(RootPassword)
            .WithDatabase("prodavnica")
            .WithResourceMapping(new FileInfo(Path.Combine(AppContext.BaseDirectory, "Database", "DDL.sql")), "/schema/")
            .Build();

        public string RootConnectionString { get; private set; } = string.Empty;

        // The restricted account the desktop app uses.
        public string AppConnectionString
        {
            get
            {
                var builder = new MySqlConnectionStringBuilder(RootConnectionString) { UserID = "prodavnica_app", Password = "prodavnica_app" };
                return builder.ConnectionString;
            }
        }

        public async Task InitializeAsync()
        {
            await container.StartAsync();

            // Run the script with the mysql client: it uses DELIMITER, which drivers do not understand.
            var result = await container.ExecAsync(new[]
            {
                "sh", "-c", $"mysql --default-character-set=utf8mb4 -uroot -p{RootPassword} prodavnica < /schema/DDL.sql"
            });
            if (result.ExitCode != 0)
            {
                throw new InvalidOperationException("Loading Model/DDL.sql failed: " + result.Stderr);
            }

            RootConnectionString = container.GetConnectionString();
            DBUtil.ConnectionString = AppConnectionString;
        }

        public Task DisposeAsync()
        {
            return container.DisposeAsync().AsTask();
        }
    }

    [CollectionDefinition(Name)]
    public sealed class DatabaseCollection : ICollectionFixture<MySqlFixture>
    {
        public const string Name = "MySQL";
    }
}
