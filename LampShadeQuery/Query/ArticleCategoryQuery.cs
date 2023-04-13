using BlogManagement.Domain.ArticleAggregate;
using BlogManagement.Infrastructure.EFCore;
using Framework.Application;
using LampShadeQuery.Contracts.ArticleAggregate;
using LampShadeQuery.Contracts.ArticleCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogDbContext _blogContext;

        public ArticleCategoryQuery(BlogDbContext blogContext) => _blogContext = blogContext;

        public List<ArticleCategoryQueryModel> GetArticleCategories() => _blogContext.ArticleCategories
            .Include(ac => ac.Articles)
            .Select(ac => new ArticleCategoryQueryModel
            {
                Name = ac.Name,
                Picture = ac.Picture,
                PictureAlt = ac.PictureAlt,
                PictureTitle = ac.PictureTitle,
                Slug = ac.Slug,
                Articles = MapArticles(ac.Articles),
                ArticlesCount = ac.Articles.Count
            }).ToList();
        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var category = _blogContext.ArticleCategories.Include(ac => ac.Articles).Select(ac => new ArticleCategoryQueryModel
            {
                Slug = ac.Slug,
                Name = ac.Name,
                Description = ac.Description,
                Picture = ac.Picture,
                PictureAlt = ac.PictureAlt,
                PictureTitle = ac.PictureTitle,
                Keywords = ac.Keywords,
                MetaDescription = ac.MetaDescription,
                CanonicalAddress = ac.CanonicalAddress,
                ArticlesCount = ac.Articles.Count,
                Articles = MapArticles(ac.Articles)
            }).FirstOrDefault(ac => ac.Slug == slug);

            if(!string.IsNullOrWhiteSpace(category.Keywords))
                category.KeywordsList = category.Keywords.Split(",").ToList();
            return category;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles) => articles.Select(ac => new ArticleQueryModel
        {
            Slug = ac.Slug,
            ShortDescription = ac.ShortDescription,
            Title = ac.Title,
            Picture = ac.Picture,
            PictureAlt = ac.PictureAlt,
            PictureTitle = ac.PictureTitle,
            PublishDate = ac.PublishDate.ToFarsi() 
        }).ToList();
    }
}
