using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAggregate;

namespace ShopManagement.Infrastructure.EFCore.Mappings
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(pp => pp.Id);
            builder.Property(pp => pp.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(pp => pp.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(pp => pp.PictureTitle).HasMaxLength(500).IsRequired();

            builder.HasOne(pp => pp.Product).WithMany(p => p.ProductPictures).HasForeignKey(pp => pp.ProductId);
        }
    }
}