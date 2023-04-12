using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.SlideAggregate;
using ShopManagement.Infrastructure.EFCore.Mappings;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}