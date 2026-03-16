using AuthenticationService.Data;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public sealed class CredentialAuthenticationService : IAuthenticationService
{
    private readonly IUserCredentialRepository _repository;
    private readonly IPasswordHashVerifier _passwordHashVerifier;
    private readonly ITokenIssuer _tokenIssuer;

    public CredentialAuthenticationService(
        IUserCredentialRepository repository,
        IPasswordHashVerifier passwordHashVerifier,
        ITokenIssuer tokenIssuer)
    {
        _repository = repository;
        _passwordHashVerifier = passwordHashVerifier;
        _tokenIssuer = tokenIssuer;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var credential = await _repository.FindByUsernameAsync(request.Username, cancellationToken);
        if (credential is null || !credential.IsActive)
        {
            throw new UnauthorizedAccessException("Credential record not found or inactive.");
        }

        if (!_passwordHashVerifier.Verify(request.Password, credential.PasswordHash))
        {
            throw new UnauthorizedAccessException("Credential verification failed.");
        }

        var (token, expiresAt) = _tokenIssuer.IssueToken(credential);
        return new LoginResponse(Token: token, ExpiresAt: expiresAt);
    }
}
