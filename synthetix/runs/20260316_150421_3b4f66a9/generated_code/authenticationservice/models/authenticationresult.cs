namespace AuthenticationService.Models;

public class AuthenticationResult
{
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}