using Microsoft.EntityFrameworkCore;
using PaymentTerminal.DataAccess.Models;

namespace PaymentTerminal.DataAccess
{
    public class PaymentTerminalContext : DbContext
    {
        public PaymentTerminalContext(DbContextOptions<PaymentTerminalContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<NewCards> NewCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
    }
}