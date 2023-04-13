using BlogManagement.Domain.ArticleAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using BlogManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlogManagement.Infrastructure.EFCore
{
    public class BlogDbContext : DbContext
    {
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = typeof(ArticleCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
