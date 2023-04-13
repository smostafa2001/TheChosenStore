using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAggregate
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        string GetSlug(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
