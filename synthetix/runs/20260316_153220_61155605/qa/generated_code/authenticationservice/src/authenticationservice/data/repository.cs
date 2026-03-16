using Dapper;
using Npgsql;

namespace AuthenticationService.Data;

public sealed class UserRepository : IUserRepository
{
    private readonly IConnectionFactory _factory;

    public UserRepository(IConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<UserRecord?> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        await using var conn = await _factory.CreateOpenConnectionAsync(ct);
        const string sql = @"select id, username, password_hash from auth_users where username = @u limit 1";
        var row = await conn.QueryFirstOrDefaultAsync<(Guid id, string username, string password_hash)>(sql, new { u = username });
        if (row == default) return null;
        return new UserRecord(row.id, row.username, row.password_hash);
    }
}
