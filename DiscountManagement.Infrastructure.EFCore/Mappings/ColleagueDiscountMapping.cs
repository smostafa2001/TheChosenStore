using DiscountManagement.Domain.ColleagueDiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mappings;

public class ColleagueDiscountMapping : IEntityTypeConfiguration<ColleagueDiscount>
{
    public void Configure(EntityTypeBuilder<ColleagueDiscount> builder)
    {
        builder.ToTable("ColleagueDiscounts");
        builder.HasKey(cd => cd.Id);
    }
}