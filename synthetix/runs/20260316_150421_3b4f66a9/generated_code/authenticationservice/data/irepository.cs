using AuthenticationService.Models;

namespace AuthenticationService.Data;

public interface IRepository
{
    User? GetUserByUsername(string username);
}