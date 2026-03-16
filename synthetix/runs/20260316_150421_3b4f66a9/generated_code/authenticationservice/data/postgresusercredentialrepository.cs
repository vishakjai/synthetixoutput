using System.Text.RegularExpressions;
using AuthenticationService.Models;
using Npgsql;

namespace AuthenticationService.Data;

public sealed class PostgresUserCredentialRepository : IUserCredentialRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;
    private readonly IConfiguration _configuration;

    public PostgresUserCredentialRepository(
        INpgsqlConnectionFactory connectionFactory,
        IConfiguration configuration)
    {
        _connectionFactory = connectionFactory;
        _configuration = configuration;
    }

    public async Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var tableName = ResolveTableName(_configuration["AUTH_DB_TABLE"]);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = $"select user_id, username, password_hash, is_active from {tableName} where username = @username limit 1;";
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("username", username);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new UserCredentialRecord(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetBoolean(3));
    }

    private static string ResolveTableName(string? configuredName)
    {
        var candidate = string.IsNullOrWhiteSpace(configuredName) ? "user_credentials" : configuredName.Trim();
        if (!Regex.IsMatch(candidate, "^[A-Za-z_][A-Za-z0-9_]*$"))
        {
            throw new InvalidOperationException("AUTH_DB_TABLE contains unsupported characters.");
        }

        return candidate;
    }
}
