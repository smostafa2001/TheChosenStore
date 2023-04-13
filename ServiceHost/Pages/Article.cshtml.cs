using LampShadeQuery.Contracts.ArticleAggregate;
using LampShadeQuery.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _categoryQuery.GetArticleCategories();
        }
    }
}
