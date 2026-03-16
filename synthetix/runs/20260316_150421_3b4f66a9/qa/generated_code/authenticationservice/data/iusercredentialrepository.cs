using AuthenticationService.Models;

namespace AuthenticationService.Data;

public interface IUserCredentialRepository
{
    Task<UserCredentialRecord?> FindByUsernameAsync(string username, CancellationToken cancellationToken);
}
