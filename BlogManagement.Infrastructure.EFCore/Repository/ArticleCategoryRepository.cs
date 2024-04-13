using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository;

public class ArticleCategoryRepository(BlogDbContext blogContext) : BaseRepository<long, ArticleCategory>(blogContext), IArticleCategoryRepository
{
    public List<ArticleCategoryViewModel> GetArticleCategories() => [.. blogContext.ArticleCategories.Select(ac => new ArticleCategoryViewModel
    {
        Id = ac.Id,
        Name = ac.Name,
    })];
    public EditArticleCategory GetDetails(long id) => blogContext.ArticleCategories.Select(ac => new EditArticleCategory
    {
        Id = ac.Id,
        Name = ac.Name,
        CanonicalAddress = ac.CanonicalAddress,
        Description = ac.Description,
        Keywords = ac.Keywords,
        MetaDescription = ac.MetaDescription,
        ShowOrder = ac.ShowOrder,
        Slug = ac.Slug,
        PictureAlt = ac.PictureAlt,
        PictureTitle = ac.PictureTitle
    }).FirstOrDefault(ac => ac.Id == id);
    public string GetSlug(long id) => blogContext.ArticleCategories.Select(ac => new { ac.Id, ac.Slug }).FirstOrDefault(ac => ac.Id == id).Slug;

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        var query = blogContext.ArticleCategories.Include(ac => ac.Articles).Select(ac => new ArticleCategoryViewModel
        {
            Id = ac.Id,
            Name = ac.Name,
            Picture = ac.Picture,
            Description = ac.Description,
            ShowOrder = ac.ShowOrder,
            CreationDate = ac.CreationDate.ToFarsi(),
            ArticlesCount = ac.Articles.Count
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(ac => ac.Name.Contains(searchModel.Name));

        return [.. query.OrderByDescending(ac => ac.Id)];
    }
}
