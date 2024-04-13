using BlogManagement.Domain.ArticleCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mappings;

public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
{
    public void Configure(EntityTypeBuilder<ArticleCategory> builder)
    {
        builder.Property(ac => ac.Name).HasMaxLength(500);
        builder.Property(ac => ac.Description).HasMaxLength(2000);
        builder.Property(ac => ac.Picture).HasMaxLength(1000);
        builder.Property(ac => ac.Slug).HasMaxLength(700);
        builder.Property(ac => ac.Keywords).HasMaxLength(100);
        builder.Property(ac => ac.MetaDescription).HasMaxLength(150);
        builder.Property(ac => ac.CanonicalAddress).HasMaxLength(1000);
        builder.Property(a => a.PictureAlt).HasMaxLength(500);
        builder.Property(a => a.PictureTitle).HasMaxLength(500);

        builder.HasMany(ac => ac.Articles).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);
    }
}
