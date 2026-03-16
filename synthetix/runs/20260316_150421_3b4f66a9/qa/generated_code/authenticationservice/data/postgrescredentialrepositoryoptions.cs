namespace AuthenticationService.Data;

public sealed class PostgresCredentialRepositoryOptions
{
    public string TableName { get; init; } = "user_credentials";
}
