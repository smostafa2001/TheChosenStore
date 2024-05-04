using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.OrderAggregate;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Application.Implementations;
using ShopManagement.Domain.ACL;
using ShopManagement.Domain.OrderAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.SlideAggregate;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.Infrastructure.InventoryACL;
using TheChosenStoreQuery.Contracts.CartAggregate;
using TheChosenStoreQuery.Contracts.ProductAggregate;
using TheChosenStoreQuery.Contracts.ProductCategoryAggregate;
using TheChosenStoreQuery.Contracts.SlideAggregate;
using TheChosenStoreQuery.Query;

namespace ShopManagement.Infrastructure.Configuration;

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

        services.AddTransient<IShopInventoryACL, ShopInventoryACL>();

        services.AddTransient<IOrderApplication, OrderApplication>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddSingleton<ICartService, CartService>();

        services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

        services.AddTransient<ICartCalculatorService, CartCalculatorService>();

        services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connectionString));
    }
}