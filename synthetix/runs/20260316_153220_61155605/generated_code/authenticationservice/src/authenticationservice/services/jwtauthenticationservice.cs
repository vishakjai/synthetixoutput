using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Data;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services;

public class JwtAuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _repo;
    private readonly byte[] _jwtKey;
    private readonly TimeSpan _tokenTtl;

    public JwtAuthenticationService(IUserRepository repo)
    {
        _repo = repo;
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET")!; // verified in Program
        _jwtKey = Encoding.UTF8.GetBytes(secret);
        var ttlEnv = Environment.GetEnvironmentVariable("TOKEN_TTL_SECONDS");
        _tokenTtl = TimeSpan.FromSeconds(int.TryParse(ttlEnv, out var s) && s > 0 ? s : 3600);
    }

    public async Task<AuthResult> AuthenticateAsync(string username, string password, CancellationToken ct)
    {
        var user = await _repo.GetUserByUsernameAsync(username, ct);
        if (user is null)
        {
            return new AuthResult(false, "User not found", null, DateTimeOffset.MinValue, Guid.Empty, username);
        }

        // Compare hashed password
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return new AuthResult(false, "Invalid password", null, DateTimeOffset.MinValue, Guid.Empty, username);
        }

        var expires = DateTimeOffset.UtcNow.Add(_tokenTtl);
        var token = IssueJwt(user.Id, user.Username, expires);
        return new AuthResult(true, null, token, expires, user.Id, user.Username);
    }

    private string IssueJwt(Guid userId, string username, DateTimeOffset expires)
    {
        var handler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username)
            }),
            Expires = expires.UtcDateTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtKey), SecurityAlgorithms.HmacSha256)
        };
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
