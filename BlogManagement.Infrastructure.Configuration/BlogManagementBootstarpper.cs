using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Application.Implementations;
using BlogManagement.Domain.ArticleCategoryAggregate;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstarpper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddDbContext<BlogDbContext>(ob => ob.UseSqlServer(connectionString));
        }
    }
}
