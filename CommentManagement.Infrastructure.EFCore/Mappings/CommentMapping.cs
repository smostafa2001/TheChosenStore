using CommentManagement.Domain.CommentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mappings;

public class CommentMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(500);
        builder.Property(c => c.Email).HasMaxLength(500);
        builder.Property(c => c.Message).HasMaxLength(1000);

        builder.HasOne(c => c.Parent).WithMany(p => p.Children).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
    }
}
