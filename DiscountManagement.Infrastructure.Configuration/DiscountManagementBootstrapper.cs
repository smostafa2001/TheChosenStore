using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using DiscountManagement.Application.Implementations;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Configuration;

public class DiscountManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
        services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

        services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
        services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

        services.AddDbContext<DiscountDbContext>(x => x.UseSqlServer(connectionString));
    }
}