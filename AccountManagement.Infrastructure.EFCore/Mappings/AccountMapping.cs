using AccountManagement.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.Fullname).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Username).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.ProfilePhoto).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.Mobile).HasMaxLength(30).IsRequired();

            builder.HasOne(a => a.Role).WithMany(r => r.Accounts).HasForeignKey(a => a.RoleId);
        }
    }
}
