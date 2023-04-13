using CommentManagement.Domain.CommentAggregate;
using CommentManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommentManagement.Infrastructure.EFCore
{
    public class CommentDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
