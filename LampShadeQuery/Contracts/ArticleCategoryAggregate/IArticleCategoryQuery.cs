using System.Collections.Generic;

namespace LampShadeQuery.Contracts.ArticleCategoryAggregate
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategories();
        ArticleCategoryQueryModel GetArticleCategory(string slug);
    }
}
