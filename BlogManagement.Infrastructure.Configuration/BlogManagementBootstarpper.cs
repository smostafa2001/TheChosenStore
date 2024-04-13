﻿using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Application.Implementations;
using BlogManagement.Domain.ArticleAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using DecorativeStoreQuery.Contracts.ArticleAggregate;
using DecorativeStoreQuery.Contracts.ArticleCategoryAggregate;
using DecorativeStoreQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration;

public class BlogManagementBootstarpper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
        services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

        services.AddTransient<IArticleApplication, ArticleApplication>();
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IArticleQuery, ArticleQuery>();
        services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

        services.AddDbContext<BlogDbContext>(ob => ob.UseSqlServer(connectionString));
    }
}
