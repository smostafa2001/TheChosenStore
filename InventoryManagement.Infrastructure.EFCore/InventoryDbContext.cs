using InventoryManagement.Domain.InventoryAggregate;
using InventoryManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryManagement.Infrastructure.EFCore
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
