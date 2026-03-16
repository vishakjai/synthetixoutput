namespace AuthenticationService.Models;

public static class AuthRoutes
{
    public const string Login = "/auth/login";
}

public sealed record LoginRequest([property: System.Text.Json.Serialization.JsonPropertyName("username")] string Username, [property: System.Text.Json.Serialization.JsonPropertyName("password")] string Password);

public sealed record LoginResponse([property: System.Text.Json.Serialization.JsonPropertyName("token")] string Token, [property: System.Text.Json.Serialization.JsonPropertyName("expires_at")] DateTimeOffset ExpiresAt);

public sealed record UserCredentialRecord(
    int UserId,
    string Username,
    string PasswordHash,
    bool IsActive);

public sealed class AuthTokenOptions
{
    public string Issuer { get; init; } = "synthetix-auth";
    public string Audience { get; init; } = "modernized-app";
    public string SigningKey { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; } = 30;
}
