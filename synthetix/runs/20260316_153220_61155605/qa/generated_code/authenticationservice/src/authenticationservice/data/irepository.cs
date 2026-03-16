namespace AuthenticationService.Data;

public sealed record UserRecord(Guid Id, string Username, string PasswordHash);

public interface IUserRepository
{
    Task<UserRecord?> GetUserByUsernameAsync(string username, CancellationToken ct);
}
