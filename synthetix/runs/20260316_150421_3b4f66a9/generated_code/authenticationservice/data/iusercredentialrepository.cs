using AuthenticationService.Models;

namespace AuthenticationService.Data;

public interface IUserCredentialRepository
{
    Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<int> IncrementFailedAttemptsAsync(int userId, CancellationToken cancellationToken);
    Task DeactivateUserAsync(int userId, CancellationToken cancellationToken);
    Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken);
}
