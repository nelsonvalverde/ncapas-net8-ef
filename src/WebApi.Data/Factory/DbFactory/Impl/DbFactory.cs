using Microsoft.Data.SqlClient;
using static WebApi.Data.Factory.DbFactory.Impl.DbSchema;

namespace WebApi.Data.Factory.DbFactory.Impl;

public class DbFactory(string connectionString) : IDbFactory
{
    private readonly string _connectionString = connectionString;
    public bool KeepOpen { get; private set; } = false;
    private SqlConnection Connection { get; set; } = default!;

    #region Async

    public async Task DataReaderAsync(Schema schema, string storedProcedure, Action<SqlDataReader> readerAction, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default)
    {
        Connection = await OpenConnectionAsync().ConfigureAwait(false);
        await DataReaderAsync(Connection, schema, storedProcedure, readerAction, parameters, cancellationToken).ConfigureAwait(false);
    }

    public async Task DataReaderAsync(SqlConnection connection, Schema schema, string storedProcedure, Action<SqlDataReader> readerAction, SqlParameter[]? parameters = null, CancellationToken cancellationToken = default)
    {
        var schemaSelected = Select(schema);
        await using var command = CreateCommand(connection, $"{schemaSelected}.{storedProcedure}", CommandType.StoredProcedure, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        while (await reader.ReadAsync(cancellationToken))
        {
            readerAction(reader);
        }
        await CloseConnectionAsync();
    }

    public async Task<DataTableCollection> DataTableCollectionAsync(Schema schema, string storedProcedure, SqlParameter[]? parameters = null)
    {
        Connection = await OpenConnectionAsync().ConfigureAwait(false);
        return await DataTableCollectionAsync(Connection, schema, storedProcedure, parameters).ConfigureAwait(false);
    }

    public async Task<DataTableCollection> DataTableCollectionAsync(SqlConnection connection, Schema schema, string storedProcedure, SqlParameter[]? parameters = null)
    {
        var schemaSelected = Select(schema);
        var dataSet = new DataSet();
        await using var command = CreateCommand(connection, $"{schemaSelected}.{storedProcedure}", CommandType.StoredProcedure, parameters);
        using var dataAdapter = new SqlDataAdapter(command);
        dataAdapter.Fill(dataSet);
        await CloseConnectionAsync();
        return dataSet.Tables;
    }

    public async Task ExecuteNonQueryAsync(Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default)
    {
        Connection = await OpenConnectionAsync().ConfigureAwait(false);
        await ExecuteNonQueryAsync(Connection, schema, storedProcedure, parameters, cancellationToken);
    }

    public async Task ExecuteNonQueryAsync(SqlConnection connection, Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default)
    {
        var schemaSelected = Select(schema);
        await using var command = CreateCommand(connection, $"{schemaSelected}.{storedProcedure}", CommandType.StoredProcedure, parameters);
        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        await CloseConnectionAsync();
    }

    public async Task<T> ExecuteScalarAsync<T>(Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default)
    {
        Connection = await OpenConnectionAsync().ConfigureAwait(false);
        return await ExecuteScalarAsync<T>(Connection, schema, storedProcedure, parameters, cancellationToken);
    }

    public async Task<T> ExecuteScalarAsync<T>(SqlConnection connection, Schema schema, string storedProcedure, SqlParameter[] parameters, CancellationToken cancellationToken = default)
    {
        var schemaSelected = Select(schema);
        await using var command = CreateCommand(connection, $"{schemaSelected}.{storedProcedure}", CommandType.StoredProcedure, parameters);
        var resultScalar = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        var result = (T)resultScalar;
        await CloseConnectionAsync();
        return result;
    }

    #endregion Async

    public async Task DisposeConnectionAsync()
    {
        KeepOpen = false;
        await Connection.CloseAsync().ConfigureAwait(false);    
        await Connection.DisposeAsync().ConfigureAwait(false);
    }

    public void DisposeConnection()
    {
        KeepOpen = false;
        Connection.Close();
        Connection.Dispose();
    }

    public SqlConnection OpenConnection()
    {
        if (Connection is { State: ConnectionState.Open } && KeepOpen)
            return Connection;

        Connection = new SqlConnection(_connectionString);
        Connection.Open();
        return Connection;
    }

    public SqlConnection OpenConnectionAndKeepOpen()
    {
        KeepOpen = true;
        return OpenConnection();
    }

    public static SqlParameter CreateTableParameter(string name, DataTable value)
    {
        var res = new SqlParameter(name, SqlDbType.Structured)
        {
            Value = value
        };
        return res;
    }

    #region Private methods

    private async Task CloseConnectionAsync()
    {
        if (Connection is { State: ConnectionState.Open } && KeepOpen)
            return;
        await DisposeConnectionAsync();
    }

    private async Task<SqlConnection> OpenConnectionAsync()
    {
        if (Connection is { State: ConnectionState.Open } && KeepOpen)
            return Connection;

        Connection = new SqlConnection(_connectionString);
        await Connection.OpenAsync();
        return Connection;
    }

    private static SqlCommand CreateCommand(SqlConnection connection, string query, CommandType commandType, SqlParameter[]? parameters)
    {
        var command = CreateSqlCommand(connection, query, commandType);
        if (parameters != null) command.Parameters.AddRange(parameters);
        return command;
    }

    private static SqlCommand CreateSqlCommand(SqlConnection connection, string query, CommandType commandType)
    {
        SqlCommand command = new(query, connection)
        {
            CommandType = commandType
        };
        return command;
    }

    #endregion Private methods
}