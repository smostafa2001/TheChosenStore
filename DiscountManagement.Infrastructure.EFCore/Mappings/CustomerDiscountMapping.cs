using DiscountManagement.Domain.CustomerDiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mappings;

public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
{
    public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
    {
        builder.ToTable("CustomerDiscounts");
        builder.HasKey(cd => cd.Id);

        builder.Property(cd => cd.Reason).HasMaxLength(500);
    }
}