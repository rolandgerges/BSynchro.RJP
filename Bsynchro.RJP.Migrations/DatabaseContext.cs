using Bsynchro.RJP.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bsynchro.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public DatabaseContext()
        {
        }
    
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures relationships
            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.SourceAccount)
                .WithMany(b => b.SourceTransactions)
                .HasForeignKey(x=>x.SourceAccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(b=>b.DestinationAccount)
                .WithMany(b=>b.DestinationTransactions)
                .HasForeignKey(x=>x.DestinationAccountId);

            modelBuilder.Entity<Account>()
                .HasOne(b=>b.Customer)
                .WithMany(b=>b.Accounts)
                .HasForeignKey(x=>x.CustomerId);
        }
    }
}