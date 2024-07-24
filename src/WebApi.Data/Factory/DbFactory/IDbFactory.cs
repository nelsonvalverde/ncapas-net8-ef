using Microsoft.Data.SqlClient;
using static WebApi.Data.Factory.DbFactory.Impl.DbSchema;

namespace WebApi.Data.Factory.DbFactory;

public interface IDbFactory
{
    #region async methods

    Task DataReaderAsync(Schema schema, string storedProcedure, Action<SqlDataReader> readerAction, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default);

    Task DataReaderAsync(SqlConnection connection, Schema schema, string storedProcedure, Action<SqlDataReader> readerAction, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default);

    Task ExecuteNonQueryAsync(Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default);

    Task ExecuteNonQueryAsync(SqlConnection connection, Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default);

    Task<T> ExecuteScalarAsync<T>(Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default);

    Task<T> ExecuteScalarAsync<T>(SqlConnection connection, Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default);

    Task<DataTableCollection> DataTableCollectionAsync(Schema schema, string storedProcedure,
        SqlParameter[]? parameters = null);

    Task DisposeConnectionAsync();

    SqlConnection OpenConnection();

    SqlConnection OpenConnectionAndKeepOpen();

    void DisposeConnection();

    #endregion async methods
}