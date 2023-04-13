using BlogManagement.Domain.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mappings
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(500);
            builder.Property(a => a.ShortDescription).HasMaxLength(1000);
            builder.Property(a => a.Picture).HasMaxLength(1000);
            builder.Property(a => a.Slug).HasMaxLength(600);
            builder.Property(a => a.Keywords).HasMaxLength(100);
            builder.Property(a => a.MetaDescription).HasMaxLength(150);
            builder.Property(a => a.CanonicalAddress).HasMaxLength(1000);
            builder.Property(a => a.PictureAlt).HasMaxLength(500);
            builder.Property(a => a.PictureTitle).HasMaxLength(500);

            builder.HasOne(a => a.Category).WithMany(ac => ac.Articles).HasForeignKey(a => a.CategoryId);
        }
    }
}
