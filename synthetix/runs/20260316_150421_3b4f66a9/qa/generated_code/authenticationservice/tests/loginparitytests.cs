using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services;

namespace AuthenticationService.Tests;

public sealed class LoginParityTests
{
    [Fact]
    public async Task Login_returns_jwt_and_expires_at_for_valid_credentials()
    {
        var repository = BuildRepositorySpy(record: BuildCredentialRecord());
        var service = BuildService(repository);

        var response = await service.LoginAsync(new LoginRequest("bank-user", "correct-password"), CancellationToken.None);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(response.Token);

        Assert.False(string.IsNullOrWhiteSpace(response.Token));
        Assert.True(response.ExpiresAt > DateTimeOffset.UtcNow);
        Assert.Equal("7", jwt.Subject);
        Assert.Equal("bank-user", jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value);
    }

    [Fact]
    public async Task Login_rejects_invalid_password_and_records_failed_attempt()
    {
        var repository = BuildRepositorySpy(record: BuildCredentialRecord());
        var service = BuildService(repository);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.LoginAsync(new LoginRequest("bank-user", "wrong-password"), CancellationToken.None));

        Assert.Equal(1, repository.RecordFailedAttemptCalls);
        Assert.Equal(0, repository.ResetFailedAttemptCalls);
    }

    [Fact]
    public async Task Login_rejects_inactive_user()
    {
        var repository = BuildRepositorySpy(record: BuildCredentialRecord(isActive: false));
        var service = BuildService(repository);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.LoginAsync(new LoginRequest("bank-user", "correct-password"), CancellationToken.None));
    }

    [Fact]
    public async Task Login_rejects_unknown_user()
    {
        var repository = BuildRepositorySpy(record: null);
        var service = BuildService(repository);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.LoginAsync(new LoginRequest("unknown-user", "correct-password"), CancellationToken.None));
    }

    [Fact]
    public async Task Login_rejects_empty_username_or_password_without_hitting_repository()
    {
        var repository = BuildRepositorySpy(record: BuildCredentialRecord());
        var service = BuildService(repository);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.LoginAsync(new LoginRequest("", ""), CancellationToken.None));

        Assert.Equal(0, repository.FindByUsernameCalls);
    }

    [Fact]
    public void PasswordHashVerifier_returns_false_for_malformed_hash()
    {
        var verifier = new PasswordHashVerifier();

        Assert.False(verifier.Verify("correct-password", "not-a-valid-hash"));
    }

    [Fact]
    public void Login_endpoint_route_is_stable()
    {
        Assert.Equal("/auth/login", AuthRoutes.Login);
    }

    [Fact]
    public async Task Login_preserves_expected_authenticated_session_behavior()
    {
        var repository = BuildRepositorySpy(record: BuildCredentialRecord());
        var service = BuildService(repository);

        var response = await service.LoginAsync(new LoginRequest("bank-user", "correct-password"), CancellationToken.None);

        Assert.False(string.IsNullOrWhiteSpace(response.Token));
        Assert.True(response.ExpiresAt > DateTimeOffset.UtcNow);
        Assert.Equal(1, repository.ResetFailedAttemptCalls);
    }

    private static CredentialAuthenticationService BuildService(RepositorySpy repository)
    {
        var tokenIssuer = new JwtTokenIssuer(new AuthTokenOptions
        {
            SigningKey = "integration-test-signing-key-1234567890",
            ExpiryMinutes = 30,
            Issuer = "synthetix-auth",
            Audience = "modernized-app",
        });
        var verifier = new PasswordHashVerifier();
        var lockoutPolicy = new DatabaseLoginLockoutPolicy(repository, new AuthLockoutOptions { FailureThreshold = 5 });
        return new CredentialAuthenticationService(repository, verifier, tokenIssuer, lockoutPolicy);
    }

    private static UserCredentialRecord BuildCredentialRecord(bool isActive = true, int failedAttemptCount = 0)
    {
        return new UserCredentialRecord(7, "bank-user", BuildStoredHash("correct-password"), isActive, failedAttemptCount);
    }

    private static RepositorySpy BuildRepositorySpy(UserCredentialRecord? record)
    {
        return new RepositorySpy(record);
    }

    private static string BuildStoredHash(string password)
    {
        var salt = Encoding.UTF8.GetBytes("synthetix-auth-salt");
        const int iterations = 100_000;
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
        return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private sealed class RepositorySpy : IUserCredentialRepository
    {
        private readonly UserCredentialRecord? _record;

        public RepositorySpy(UserCredentialRecord? record)
        {
            _record = record;
        }

        public int FindByUsernameCalls { get; private set; }
        public int RecordFailedAttemptCalls { get; private set; }
        public int ResetFailedAttemptCalls { get; private set; }

        public Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            FindByUsernameCalls += 1;
            return Task.FromResult(_record is not null && _record.Username == username ? _record : null);
        }

        public Task RecordFailedAttemptAsync(int userId, int failureThreshold, CancellationToken cancellationToken)
        {
            RecordFailedAttemptCalls += 1;
            return Task.CompletedTask;
        }

        public Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken)
        {
            ResetFailedAttemptCalls += 1;
            return Task.CompletedTask;
        }
    }
}
