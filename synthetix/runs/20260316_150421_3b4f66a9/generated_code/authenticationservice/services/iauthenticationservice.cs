using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface IAuthenticationService
{
    AuthenticationResult Authenticate(LoginRequest request);
}