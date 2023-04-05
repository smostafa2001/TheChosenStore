using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SlideAggregate;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(s => s.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(s => s.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(s => s.Heading).HasMaxLength(255).IsRequired();
            builder.Property(s => s.Title).HasMaxLength(255);
            builder.Property(s => s.Text).HasMaxLength(255);
            builder.Property(s => s.BtnText).HasMaxLength(50).IsRequired();
        }
    }
}
