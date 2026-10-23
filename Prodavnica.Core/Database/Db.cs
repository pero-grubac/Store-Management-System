using MySqlConnector;

namespace Prodavnica.Database
{
    // Small helpers so every repository opens, parameterises and maps queries the same way.
    internal static class Db
    {
        public static async Task<MySqlConnection> OpenAsync()
        {
            var connection = DBUtil.GetConnection();
            await connection.OpenAsync();
            return connection;
        }

        public static MySqlCommand Command(MySqlConnection connection, string sql, (string Name, object? Value)[] args, MySqlTransaction? transaction = null)
        {
            var command = new MySqlCommand(sql, connection, transaction);
            foreach (var (name, value) in args)
            {
                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
            }
            return command;
        }

        public static async Task<List<T>> QueryAsync<T>(string sql, Func<MySqlDataReader, T> map, params (string, object?)[] args)
        {
            try
            {
                await using var connection = await OpenAsync();
                await using var command = Command(connection, sql, args);
                await using var reader = await command.ExecuteReaderAsync();
                var list = new List<T>();
                while (await reader.ReadAsync())
                {
                    list.Add(map(reader));
                }
                return list;
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        public static async Task<T?> SingleAsync<T>(string sql, Func<MySqlDataReader, T> map, params (string, object?)[] args) where T : class
        {
            return (await QueryAsync(sql, map, args)).FirstOrDefault();
        }

        public static async Task<object?> ScalarAsync(string sql, params (string, object?)[] args)
        {
            try
            {
                await using var connection = await OpenAsync();
                await using var command = Command(connection, sql, args);
                object? value = await command.ExecuteScalarAsync();
                return value is DBNull ? null : value;
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        public static async Task<int> ExecuteAsync(string sql, params (string, object?)[] args)
        {
            try
            {
                await using var connection = await OpenAsync();
                await using var command = Command(connection, sql, args);
                return await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        // Runs the page query and the matching COUNT(*) query with the same parameters.
        public static async Task<PagedResult<T>> PageAsync<T>(string selectSql, string countSql, Func<MySqlDataReader, T> map, PageRequest page, params (string, object?)[] args)
        {
            var pageArgs = args.Concat(new (string, object?)[] { ("@limit", page.SafePageSize), ("@offset", page.Offset) }).ToArray();
            List<T> items = await QueryAsync(selectSql + " LIMIT @limit OFFSET @offset", map, pageArgs);
            int total = Convert.ToInt32(await ScalarAsync(countSql, args));
            return new PagedResult<T>(items, total, page.SafePage, page.SafePageSize);
        }

        public static string Text(this MySqlDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
        }

        public static byte[]? Bytes(this MySqlDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : (byte[])reader.GetValue(ordinal);
        }

        public static decimal Decimal(this MySqlDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? 0m : reader.GetDecimal(ordinal);
        }
    }
}
