using Microsoft.EntityFrameworkCore;
using Portal.DataAccess.Models;

namespace Portal.DataAccess
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<NewCards> NewCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
    }
}