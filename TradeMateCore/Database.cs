using Microsoft.Data.SqlClient;

namespace TradeMateCore;

internal static class Database
{
    private static SqlConnection? _connection = null;

    internal static SqlConnection GetConnection()
    {
        if (_connection != null) return _connection;

        _connection = new SqlConnection("Server=tcp:504234.database.windows.net,1433;Initial Catalog=TradeMate;Persist Security Info=False;User ID=CloudSA75d3553e;Password=Database123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return _connection;
    }
}