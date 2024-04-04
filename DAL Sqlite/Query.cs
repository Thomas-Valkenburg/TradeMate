using Dapper;
using Microsoft.Data.Sqlite; 

namespace DAL_Sqlite;

public static class Query
{
    public static async Task<List<T>?> ExecuteReadMany<T>(string query)
    {
        return await QueryRead<List<T>>(query, false);
    }
    public static async Task<T?> ExecuteReadFirst<T>(string query)
    {
        return await QueryRead<T>(query, true);
    }

    private static async Task<T?> QueryRead<T>(string query, bool single)
    {
        var connection = DatabaseConnection.GetConnection();
        T? obj;
        
        try
        {
            await connection.OpenAsync();

            if (single)
            {
                obj = await connection.QueryFirstOrDefaultAsync<T>(query);
            }
            else
            {
                var data = await connection.QueryAsync<T>(query);
                obj = (data as List<T>)!.FirstOrDefault();
            }
            
            await connection.CloseAsync();
        }
        catch (SqliteException)
        {
            return default;
        }

        return obj;
    }
}