using Common.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Application.Implementations;
using InventoryManagement.Domain.InventoryAggregate;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheChosenStoreQuery.Contracts.InventoryAggregate;
using TheChosenStoreQuery.Query;

namespace InventoryManagement.Infrastructure.Configuration;

public class InventoryManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IInventoryApplication, InventoryApplication>();
        services.AddTransient<IInventoryRepository, InventoryRepository>();

        services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

        services.AddTransient<IInventoryQuery, InventoryQuery>();

        services.AddDbContext<InventoryDbContext>(ob => ob.UseSqlServer(connectionString));
    }
}
