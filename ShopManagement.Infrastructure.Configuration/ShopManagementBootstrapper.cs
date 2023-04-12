using LampShadeQuery.Contracts.ProductAggregate;
using LampShadeQuery.Contracts.ProductCategoryAggregate;
using LampShadeQuery.Contracts.SlideAggregate;
using LampShadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.CommentAggregate;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Application.Implementations;
using ShopManagement.Domain.CommentAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.SlideAggregate;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Infrastructure.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}