﻿using InventoryManagement.Application.Contracts;
using InventoryManagement.Application.Implementations;
using InventoryManagement.Domain.InventoryAggregate;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddDbContext<InventoryDbContext>(ob => ob.UseSqlServer(connectionString));
        }
    }
}