using AuthenticationService.Models;

namespace AuthenticationService.Data;

public interface IUserCredentialRepository
{
    Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken);
    Task RecordFailedAttemptAsync(int userId, int failureThreshold, CancellationToken cancellationToken);
    Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken);
}
