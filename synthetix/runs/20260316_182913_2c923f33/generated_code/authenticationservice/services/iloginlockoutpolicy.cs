using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface ILoginLockoutPolicy
{
    Task RegisterFailureAsync(UserCredentialRecord credential, CancellationToken cancellationToken);
    Task ResetAsync(UserCredentialRecord credential, CancellationToken cancellationToken);
}
