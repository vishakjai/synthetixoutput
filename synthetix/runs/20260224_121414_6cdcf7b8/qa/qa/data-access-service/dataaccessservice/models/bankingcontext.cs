using Microsoft.EntityFrameworkCore;

namespace DataAccessService.Models
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {
        }

        // Define DbSets for your entities, e.g.,
        // public DbSet<Customer> Customers { get; set; }
    }
}