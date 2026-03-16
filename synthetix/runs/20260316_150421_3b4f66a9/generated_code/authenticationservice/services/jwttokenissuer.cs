using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services;

public interface ITokenIssuer
{
    (string Token, DateTimeOffset ExpiresAt) IssueToken(UserCredentialRecord credential);
}

public sealed class JwtTokenIssuer : ITokenIssuer
{
    private readonly AuthTokenOptions _options;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly SigningCredentials _signingCredentials;

    public JwtTokenIssuer(AuthTokenOptions options)
    {
        _options = options;
        var keyBytes = Encoding.UTF8.GetBytes(_options.SigningKey);
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256);
    }

    public (string Token, DateTimeOffset ExpiresAt) IssueToken(UserCredentialRecord credential)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_options.ExpiryMinutes);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, credential.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, credential.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                ]),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            Expires = expiresAt.UtcDateTime,
            SigningCredentials = _signingCredentials,
        };
        var token = _tokenHandler.CreateToken(descriptor);
        return (_tokenHandler.WriteToken(token), expiresAt);
    }
}
