using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DAL_Account_Sqlite;

public static class DatabaseConnection
{
    private static SqliteConnection? _connection;

    private static SqliteConnection CreateConnection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return new SqliteConnection(configuration.GetConnectionString("AccountConnection")?.Replace("~", AppContext.BaseDirectory));
    }

    public static SqliteConnection GetConnection()
    {
        return _connection ??= CreateConnection();
    }
}