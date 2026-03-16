using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
