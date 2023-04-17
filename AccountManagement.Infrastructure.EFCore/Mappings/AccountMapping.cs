using AccountManagement.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.Fullname).HasMaxLength(100);
            builder.Property(a => a.Username).HasMaxLength(100);
            builder.Property(a => a.Password).HasMaxLength(1000);
            builder.Property(a => a.Password).HasMaxLength(1000);
            builder.Property(a => a.ProfilePhoto).HasMaxLength(1000);
            builder.Property(a => a.Mobile).HasMaxLength(30);
        }
    }
}
