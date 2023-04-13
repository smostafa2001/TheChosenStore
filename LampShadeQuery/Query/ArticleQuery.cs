using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Framework.Application;
using LampShadeQuery.Contracts.ArticleAggregate;
using LampShadeQuery.Contracts.CommentAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogDbContext _blogContext;
        private readonly CommentDbContext _commentContext;
        public ArticleQuery(BlogDbContext blogContext, CommentDbContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _blogContext.Articles.Include(a => a.Category).Where(a => a.PublishDate <= DateTime.Now).Select(a => new ArticleQueryModel
            {
                Id = a.Id,
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

            var comments = _commentContext.Comments
                .Where(c => c.Type == CommentType.Article && !c.IsCanceled && c.IsConfirmed && c.OwnerRecordId == article.Id)
                .Select(c => new CommentQueryModel
                {
                    Id = c.Id,
                    Message = c.Message,
                    Name = c.Name,
                    CreationDate = c.CreationDate.ToFarsi(),
                    ParentId = c.ParentId
                }).OrderBy(c => c.Id).ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = comments.FirstOrDefault(c => c.Id == comment.ParentId)?.Name;

            }

            article.Comments = comments;
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
