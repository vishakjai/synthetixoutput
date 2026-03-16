namespace AuthenticationService.Services;

public sealed record AuthResult(bool Success, string? Error, string? Token, DateTimeOffset ExpiresAt, Guid UserId, string Username);

public interface IAuthenticationService
{
    Task<AuthResult> AuthenticateAsync(string username, string password, CancellationToken ct);
}
