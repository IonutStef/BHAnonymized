using Anonymized.Assessment.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Anonymized.Assessment.Data
{
    public class AccountManagementContext : DbContext 
    {
        public AccountManagementContext(DbContextOptions<AccountManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountManagementContext).Assembly);
        }

        public virtual DbSet<CustomerDto> Customers { get; set; }

        public virtual DbSet<AccountDto> Accounts { get; set; }

        public virtual DbSet<TransactionDto> Transactions { get; set; }
    }
}