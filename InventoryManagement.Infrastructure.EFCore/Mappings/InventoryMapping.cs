using InventoryManagement.Domain.InventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mappings;

public class InventoryMapping : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");
        builder.HasKey(i => i.Id);

        builder.OwnsMany(i => i.Operations, modelBuilder =>
        {
            modelBuilder.ToTable("InventoryOperations");
            modelBuilder.HasKey(io => io.Id);
            modelBuilder.Property(io => io.Description).HasMaxLength(1000);
            modelBuilder.WithOwner(io => io.Inventory).HasForeignKey(io => io.InventoryId);
        });
    }
}
