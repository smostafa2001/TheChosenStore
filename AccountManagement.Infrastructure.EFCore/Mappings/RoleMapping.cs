using AccountManagement.Domain.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(r => r.Accounts).WithOne(a => a.Role).HasForeignKey(a => a.RoleId);
            builder.OwnsMany(r => r.Permissions, navigationBuilder =>
            {
                navigationBuilder.HasKey(p => p.Id);
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.Ignore(p => p.Name);
                navigationBuilder.WithOwner(p => p.Role);
            });
        }
    }
}
