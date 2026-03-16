namespace AuthenticationService.Data;

public sealed class PostgresCredentialRepositoryOptions
{
    private static readonly System.Text.RegularExpressions.Regex ValidTableName =
        new("^[A-Za-z_][A-Za-z0-9_]*$", System.Text.RegularExpressions.RegexOptions.Compiled);

    private string _tableName = "user_credentials";

    public string TableName
    {
        get => _tableName;
        init
        {
            var candidate = string.IsNullOrWhiteSpace(value) ? "user_credentials" : value.Trim();
            _tableName = ValidTableName.IsMatch(candidate)
                ? candidate
                : throw new ArgumentException($"Invalid table name: {value}");
        }
    }
}
