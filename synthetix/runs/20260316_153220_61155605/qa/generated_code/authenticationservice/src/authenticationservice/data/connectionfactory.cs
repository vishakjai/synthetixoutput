using System.Data;
using Npgsql;

namespace AuthenticationService.Data;

public interface IConnectionFactory
{
    ValueTask<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken ct = default);
}

public sealed class NpgsqlConnectionFactory : IConnectionFactory
{
    private readonly string _cs;

    public NpgsqlConnectionFactory()
    {
        _cs = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")
              ?? throw new InvalidOperationException("POSTGRES_CONNECTION_STRING env var must be set.");
    }

    public async ValueTask<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken ct = default)
    {
        var conn = new NpgsqlConnection(_cs);
        await conn.OpenAsync(ct);
        return conn;
    }
}
