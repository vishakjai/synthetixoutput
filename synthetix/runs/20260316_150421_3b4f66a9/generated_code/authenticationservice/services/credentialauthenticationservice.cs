using AuthenticationService.Data;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public sealed class CredentialAuthenticationService : IAuthenticationService
{
    private readonly IUserCredentialRepository _repository;
    private readonly IPasswordHashVerifier _passwordHashVerifier;
    private readonly ITokenIssuer _tokenIssuer;
    private readonly ILoginLockoutPolicy _lockoutPolicy;

    public CredentialAuthenticationService(
        IUserCredentialRepository repository,
        IPasswordHashVerifier passwordHashVerifier,
        ITokenIssuer tokenIssuer,
        ILoginLockoutPolicy lockoutPolicy)
    {
        _repository = repository;
        _passwordHashVerifier = passwordHashVerifier;
        _tokenIssuer = tokenIssuer;
        _lockoutPolicy = lockoutPolicy;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new UnauthorizedAccessException("Credentials are required.");
        }

        var username = request.Username.Trim();
        if (username.Length > 256 || request.Password.Length > 256)
        {
            throw new UnauthorizedAccessException("Credentials exceed allowed bounds.");
        }

        var credential = await _repository.FindByUsernameAsync(username, cancellationToken);
        if (credential is null || !credential.IsActive)
        {
            throw new UnauthorizedAccessException("Credential record not found or inactive.");
        }

        if (!_passwordHashVerifier.Verify(request.Password, credential.PasswordHash))
        {
            await _lockoutPolicy.RegisterFailureAsync(credential, cancellationToken);
            throw new UnauthorizedAccessException("Credential verification failed.");
        }

        await _lockoutPolicy.ResetAsync(credential, cancellationToken);
        var (token, expiresAt) = _tokenIssuer.IssueToken(credential);
        return new LoginResponse(Token: token, ExpiresAt: expiresAt);
    }
}
