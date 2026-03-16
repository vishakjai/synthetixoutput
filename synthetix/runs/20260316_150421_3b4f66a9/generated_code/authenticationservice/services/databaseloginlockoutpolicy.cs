using AuthenticationService.Data;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public sealed class DatabaseLoginLockoutPolicy : ILoginLockoutPolicy
{
    private readonly IUserCredentialRepository _repository;
    private readonly AuthLockoutOptions _options;

    public DatabaseLoginLockoutPolicy(IUserCredentialRepository repository, AuthLockoutOptions options)
    {
        _repository = repository;
        _options = options;
    }

    public Task RegisterFailureAsync(UserCredentialRecord credential, CancellationToken cancellationToken)
    {
        return _repository.RecordFailedAttemptAsync(credential.UserId, _options.FailureThreshold, cancellationToken);
    }

    public Task ResetAsync(UserCredentialRecord credential, CancellationToken cancellationToken)
    {
        return _repository.ResetFailedAttemptsAsync(credential.UserId, cancellationToken);
    }
}
