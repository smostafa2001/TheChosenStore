using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Application.Implementations;
using AccountManagement.Domain.AccountAggregate;
using AccountManagement.Domain.RoleAggregate;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountDbContext>(ob => ob.UseSqlServer(connectionString));
        }
    }
}
