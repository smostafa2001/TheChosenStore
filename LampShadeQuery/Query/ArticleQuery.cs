using BlogManagement.Infrastructure.EFCore;
using Framework.Application;
using LampShadeQuery.Contracts.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogDbContext _blogContext;

        public ArticleQuery(BlogDbContext blogContext) => _blogContext = blogContext;

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _blogContext.Articles.Include(a => a.Category).Where(a => a.PublishDate <= DateTime.Now).Select(a => new ArticleQueryModel
            {
                CategoryName = a.Category.Name,
                CategorySlug = a.Category.Slug,
                Slug = a.Slug,
                CanonicalAddress = a.CanonicalAddress,
                Description = a.Description,
                Keywords = a.Keywords,
                MetaDescription = a.MetaDescription,
                Picture = a.Picture,
                PictureAlt = a.PictureAlt,
                PictureTitle = a.PictureTitle,
                PublishDate = a.PublishDate.ToFarsi(),
                ShortDescription = a.ShortDescription,
                Title = a.Title
            }).FirstOrDefault(a => a.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles() => _blogContext.Articles.Include(a => a.Category).Where(a => a.PublishDate <= DateTime.Now).Select(a => new ArticleQueryModel
        {
            Title = a.Title,
            Slug = a.Slug,
            Picture = a.Picture,
            PictureAlt = a.PictureAlt,
            PictureTitle = a.PictureTitle,
            PublishDate = a.PublishDate.ToFarsi(),
            ShortDescription = a.ShortDescription
        }).ToList();
    }
}
