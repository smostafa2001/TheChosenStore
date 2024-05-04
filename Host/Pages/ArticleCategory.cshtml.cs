using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChosenStoreQuery.Contracts.ArticleAggregate;
using TheChosenStoreQuery.Contracts.ArticleCategoryAggregate;

namespace Host.Pages;

public class ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery) : PageModel
{
    public ArticleCategoryQueryModel? ArticleCategory { get; set; }
    public List<ArticleCategoryQueryModel>? ArticleCategories { get; set; }
    public List<ArticleQueryModel>? LatestArticles { get; set; }

    public void OnGet(string id)
    {
        ArticleCategory = articleCategoryQuery.GetArticleCategory(id);
        ArticleCategories = articleCategoryQuery.GetArticleCategories();
        LatestArticles = articleQuery.GetLatestArticles();
    }
}
