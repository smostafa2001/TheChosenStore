using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Domain.ArticleAggregate;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository;

public class ArticleRepository(BlogDbContext context) : BaseRepository<long, Article>(context), IArticleRepository
{
    public EditArticle GetDetails(long id) => context.Articles.Select(a => new EditArticle
    {
        Id = a.Id,
        Title = a.Title,
        ShortDescription = a.ShortDescription,
        Description = a.Description,
        PictureAlt = a.PictureAlt,
        PictureTitle = a.PictureTitle,
        PublishDate = a.PublishDate.ToString(CultureInfo.InvariantCulture),
        Slug = a.Slug,
        Keywords = a.Keywords,
        MetaDescription = a.MetaDescription,
        CanonicalAddress = a.CanonicalAddress,
        CategoryId = a.CategoryId
    }).FirstOrDefault(a => a.Id == id);
    public Article GetWithCategory(long id) => context.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == id);

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = context.Articles.Select(a => new ArticleViewModel
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Picture = a.Picture,
            Category = a.Category.Name,
            PublishDate = a.PublishDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            query = query.Where(a => a.Title.Contains(searchModel.Title));

        if (searchModel.CategoryId > 0)
            query = query.Where(a => a.CategoryId == searchModel.CategoryId);

        return [.. query.OrderByDescending(a => a.Id)];
    }
}
