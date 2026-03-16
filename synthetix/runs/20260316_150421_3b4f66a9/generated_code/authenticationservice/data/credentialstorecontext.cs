using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data;

public class CredentialStoreContext : DbContext
{
    public CredentialStoreContext(DbContextOptions<CredentialStoreContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}