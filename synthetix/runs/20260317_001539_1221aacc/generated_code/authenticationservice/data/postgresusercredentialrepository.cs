using AuthenticationService.Models;
using Npgsql;

namespace AuthenticationService.Data;

public sealed class PostgresUserCredentialRepository : IUserCredentialRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;
    private readonly PostgresCredentialRepositoryOptions _options;

    public PostgresUserCredentialRepository(
        INpgsqlConnectionFactory connectionFactory,
        PostgresCredentialRepositoryOptions options)
    {
        _connectionFactory = connectionFactory;
        _options = options;
    }

    public async Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText =
            $"select user_id, username, password_hash, is_active, coalesce(failed_attempt_count, 0) " +
            $"from {_options.TableName} where username = @username limit 1;";
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
            reader.GetBoolean(3),
            reader.GetInt32(4));
    }

    internal static string BuildIncrementFailedAttemptsCommandText(string tableName) => $@"
            update {tableName}
               set failed_attempt_count = coalesce(failed_attempt_count, 0) + 1
             where user_id = @userId
         returning coalesce(failed_attempt_count, 0);";

    internal static string BuildDeactivateUserCommandText(string tableName) =>
        $"update {tableName} set is_active = false where user_id = @userId;";

    internal static string BuildResetFailedAttemptsCommandText(string tableName) =>
        $"update {tableName} set failed_attempt_count = 0 where user_id = @userId;";

    public async Task<int> IncrementFailedAttemptsAsync(int userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = BuildIncrementFailedAttemptsCommandText(_options.TableName);
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("userId", userId);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result is int count ? count : Convert.ToInt32(result);
    }

    public async Task DeactivateUserAsync(int userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = BuildDeactivateUserCommandText(_options.TableName);
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("userId", userId);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = BuildResetFailedAttemptsCommandText(_options.TableName);
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("userId", userId);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
