using System.Text.Json.Serialization;

namespace AuthenticationService.Models;

public sealed class LoginRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}

public sealed class LoginResponse
{
    [JsonPropertyName("token")]
    public required string Token { get; init; }

    // Targeted remediation: use expires_at exactly as the property name in JSON
    [JsonPropertyName("expires_at")]
    public required DateTimeOffset ExpiresAt { get; init; }

    [JsonPropertyName("user_id")]
    public required Guid UserId { get; init; }

    [JsonPropertyName("username")]
    public required string Username { get; init; }
}
