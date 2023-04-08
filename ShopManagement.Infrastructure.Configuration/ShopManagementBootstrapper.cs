using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Application.Implementations;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.SlideAggregate;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Query;
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

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}