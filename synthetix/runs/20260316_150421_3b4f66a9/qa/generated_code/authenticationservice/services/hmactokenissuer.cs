using System.Security.Cryptography;
using System.Text;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface ITokenIssuer
{
    (string Token, DateTimeOffset ExpiresAt) IssueToken(UserCredentialRecord credential);
}

public sealed class HmacTokenIssuer : ITokenIssuer
{
    private readonly AuthTokenOptions _options;

    public HmacTokenIssuer(AuthTokenOptions options)
    {
        _options = options;
    }

    public (string Token, DateTimeOffset ExpiresAt) IssueToken(UserCredentialRecord credential)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_options.ExpiryMinutes);
        var payload = $"{credential.Username}|{credential.UserId}|{expiresAt.ToUnixTimeSeconds()}|{_options.Issuer}";
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.SigningKey));
        var signature = Base64UrlEncode(hmac.ComputeHash(Encoding.UTF8.GetBytes(payload)));
        var token = Base64UrlEncode(Encoding.UTF8.GetBytes($"{payload}|{signature}"));
        return (token, expiresAt);
    }

    private static string Base64UrlEncode(byte[] bytes) =>
        Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
}
