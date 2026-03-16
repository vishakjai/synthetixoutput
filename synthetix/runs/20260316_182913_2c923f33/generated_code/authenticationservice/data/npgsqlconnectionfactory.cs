using Npgsql;

namespace AuthenticationService.Data;

public interface INpgsqlConnectionFactory
{
    Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken);
}

public sealed class NpgsqlConnectionFactory : INpgsqlConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString =
            configuration["AUTH_DB_CONNECTION_STRING"]
            ?? configuration.GetConnectionString("AuthenticationDatabase")
            ?? throw new InvalidOperationException("AUTH_DB_CONNECTION_STRING is required.");
    }

    public async Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
