using AccountManagement.Domain.AccountAggregate;
using AccountManagement.Domain.RoleAggregate;
using AccountManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountMapping).Assembly);
    }
}