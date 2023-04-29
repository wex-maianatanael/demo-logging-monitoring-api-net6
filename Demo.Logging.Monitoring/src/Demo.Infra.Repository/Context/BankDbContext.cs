using Demo.Domain.Entities;
using Demo.Infra.Repository.DataSeed;
using Demo.Infra.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infra.Repository.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.StartDataSeed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
