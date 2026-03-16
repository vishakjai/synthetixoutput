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

    public async Task RecordFailedAttemptAsync(int userId, int failureThreshold, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = $@"
            update {_options.TableName}
               set failed_attempt_count = coalesce(failed_attempt_count, 0) + 1,
                   is_active = case
                       when coalesce(failed_attempt_count, 0) + 1 >= @failureThreshold then false
                       else is_active
                   end
             where user_id = @userId;";
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("failureThreshold", failureThreshold);
        command.Parameters.AddWithValue("userId", userId);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var commandText = $"update {_options.TableName} set failed_attempt_count = 0 where user_id = @userId;";
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("userId", userId);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
