using LampShadeQuery.Contracts.ArticleAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatestArticlesViewComponent(IArticleQuery productQuery) => _articleQuery = productQuery;

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetLatestArticles();
            return View(articles);
        }
    }
}
