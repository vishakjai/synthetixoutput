using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data;

public class Repository : IRepository
{
    private readonly CredentialStoreContext _context;

    public Repository(CredentialStoreContext context)
    {
        _context = context;
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.SingleOrDefault(u => u.Username == username);
    }
}