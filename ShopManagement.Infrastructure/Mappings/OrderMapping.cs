using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAggregate;

namespace ShopManagement.Infrastructure.EFCore.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.IssueTrackingNo).HasMaxLength(10);
            builder.OwnsMany(o => o.Items, navBuilder =>
            {
                navBuilder.ToTable("OrderItems");
                navBuilder.HasKey(i => i.Id);
                navBuilder.WithOwner(i => i.Order).HasForeignKey(i => i.OrderId);
            });
        }
    }
}
