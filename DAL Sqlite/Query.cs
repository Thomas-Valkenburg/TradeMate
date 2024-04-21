using Dapper;
using Dapper.Contrib.Extensions;
using Domain;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace DAL_Sqlite;

public static class Query
{
    #region Read

    public static async Task<List<T>?> ReadManyAsync<T>(string query) where T : class =>
        await QueryReadAsync<List<T>>(query, false);

    public static List<T>? ReadMany<T>(string query) where T : class =>
        QueryReadAsync<List<T>>(query, false).GetAwaiter().GetResult();

    public static async Task<T?> ReadFirstAsync<T>(string query) where T : class =>
        await QueryReadAsync<T>(query, true);

    public static T? ReadFirst<T>(string query) where T : class =>
        QueryReadAsync<T>(query, true).GetAwaiter().GetResult();

    private static async Task<T?> QueryReadAsync<T>(string query, bool single) where T : class
    {
        var connection = DatabaseConnection.GetConnection();
        T? obj;

        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            if (single)
            {
                obj = await connection.QueryFirstOrDefaultAsync<T>(query, transaction);
            }
            else
            {
                var data = await connection.QueryAsync<T>(query, transaction);
                obj = (data as List<T>)!.FirstOrDefault();
            }

            await transaction.CommitAsync();
        }
        catch (SqliteException e)
        {
            _ = await ErrorHandler(e, connection, transaction);

            return default;
        }

        return obj;
    }

    #endregion

    #region Insert / Update / Delete

    private enum NonQueryType
    {
        Insert,
        Update,
        Delete
    }

    public static async Task<Result> InsertAsync<T>(T obj) where T : class =>
        await ExecuteNonQuery(obj, NonQueryType.Insert);

    public static Result Insert<T>(T obj) where T : class =>
        ExecuteNonQuery(obj, NonQueryType.Insert).GetAwaiter().GetResult();

    public static async Task<Result> UpdateAsync<T>(T obj) where T : class =>
        await ExecuteNonQuery(obj, NonQueryType.Update);

    public static Result Update<T>(T obj) where T : class =>
        ExecuteNonQuery(obj, NonQueryType.Update).GetAwaiter().GetResult();

    public static async Task<Result> DeleteAsync<T>(T obj) where T : class =>
        await ExecuteNonQuery(obj, NonQueryType.Delete);

    public static Result Delete<T>(T obj) where T : class =>
        ExecuteNonQuery(obj, NonQueryType.Delete).GetAwaiter().GetResult();

    private static async Task<Result> ExecuteNonQuery<T>(T obj, NonQueryType type) where T : class
    {
        var connection = DatabaseConnection.GetConnection();

        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            object _ = type switch
            {
                    // INSERT INTO {TableName} ({List of property names}) VALUES ({List of property values})
                    // EG: INSERT INTO Customer (Name, Email) VALUES ('Thomas', 'thomas@trademate.com')
                NonQueryType.Insert => await connection.InsertAsync(obj, transaction),
                    // UPDATE {TableName} SET {List(Property name = property value)} WHERE {PrimaryKey} = {PrimaryKeyValue}
                    // EG: UPDATE Customer SET Name = 'Thomas', Email = 'thomas@trademate.nl' WHERE Id = 1
                NonQueryType.Update => await connection.UpdateAsync(obj, transaction),
                    // DELETE FROM {TableName} WHERE {PrimaryKey} = {PrimaryKeyValue}
                    // EG: DELETE FROM Customer WHERE Id = 1
                NonQueryType.Delete => await connection.DeleteAsync(obj, transaction),
                _                   => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            await transaction.CommitAsync();
        }
        catch (SqliteException e)
        {
            return await ErrorHandler(e, connection, transaction);
        }

        return Result.FromSuccess();
    }

    #endregion

    #region ErrorHandler
    
    private static async Task<Result> ErrorHandler(SqliteException e, DbConnection connection,
        DbTransaction transaction)
    {
        await transaction.RollbackAsync();

        await connection.CloseAsync();

        Console.WriteLine(e.Message);
        return e.SqliteErrorCode switch
        {
            1  => throw e,
            19 => Result.FromError(ErrorType.Duplicate, e.Message, e.Message.Split(' ').Last().Replace("'.", "")),
            _  => Result.FromError(ErrorType.NotFound, e.Message, "")
        };
    }

    #endregion
}