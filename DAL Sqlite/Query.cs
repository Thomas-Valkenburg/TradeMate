using Dapper;

namespace DAL_Sqlite;

public static class Query
{
    public static async Task<List<T>> Execute<T>(string query)
    {
        var     connection = DatabaseConnection.GetConnection();
        List<T> tList;
        
        try
        {
            await connection.OpenAsync();
            tList = connection.Query<T>(query).ToList();
            await connection.CloseAsync();
        }
        // ReSharper disable once RedundantCatchClause
        catch (Exception)
        {
            throw;
        }

        return tList;
    }

    public static async Task<T> ExecuteFirst<T>(string query)
    {
        var connection = DatabaseConnection.GetConnection();
        T obj;
        
        try
        {
            await connection.OpenAsync();
            obj = connection.QuerySingle<T>(query);
            await connection.CloseAsync();
        }
        // ReSharper disable once RedundantCatchClause
        catch (Exception)
        {
            throw;
        }

        return obj;
    }
}