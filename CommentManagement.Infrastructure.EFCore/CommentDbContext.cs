using CommentManagement.Domain.CommentAggregate;
using CommentManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommentManagement.Infrastructure.EFCore;

public class CommentDbContext(DbContextOptions<CommentDbContext> options) : DbContext(options)
{
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(CommentMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
