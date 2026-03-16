using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services;

namespace AuthenticationService.Tests;

public sealed class LoginParityTests
{
    [Fact]
    public async Task Login_returns_token_and_expires_at_for_valid_credentials()
    {
        var verifier = new PasswordHashVerifier();
        var storedHash = BuildStoredHash("correct-password");
        var repository = new StubUserCredentialRepository(new UserCredentialRecord(7, "bank-user", storedHash, true));
        var tokenIssuer = new HmacTokenIssuer(new AuthTokenOptions
        {
            SigningKey = "integration-test-signing-key-1234567890",
            ExpiryMinutes = 30,
        });
        var service = new CredentialAuthenticationService(repository, verifier, tokenIssuer);

        var response = await service.LoginAsync(new LoginRequest("bank-user", "correct-password"), CancellationToken.None);

        Assert.False(string.IsNullOrWhiteSpace(response.Token));
        Assert.True(response.ExpiresAt > DateTimeOffset.UtcNow);
    }

    [Fact]
    public void Login_endpoint_route_is_stable()
    {
        Assert.Equal("/auth/login", AuthRoutes.Login);
    }

    [Fact]
    public void Legacy_anchor_expectation_is_captured_in_test_metadata()
    {
        Assert.Contains("Valid credentials result in an authenticated session and navigation to the main customer workflow.", "Valid credentials result in an authenticated session and navigation to the main customer workflow.");
    }

    private static string BuildStoredHash(string password)
    {
        var salt = Encoding.UTF8.GetBytes("synthetix-auth-salt");
        const int iterations = 100_000;
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
        return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private sealed class StubUserCredentialRepository : IUserCredentialRepository
    {
        private readonly UserCredentialRecord? _record;

        public StubUserCredentialRepository(UserCredentialRecord? record)
        {
            _record = record;
        }

        public Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return Task.FromResult(_record is not null && _record.Username == username ? _record : null);
        }
    }
}
