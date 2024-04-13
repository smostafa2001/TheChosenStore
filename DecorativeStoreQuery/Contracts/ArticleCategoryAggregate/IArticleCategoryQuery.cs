using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.ArticleCategoryAggregate;

public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
    ArticleCategoryQueryModel GetArticleCategory(string slug);
}
