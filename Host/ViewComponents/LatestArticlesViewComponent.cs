using DecorativeStoreQuery.Contracts.ArticleAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class LatestArticlesViewComponent(IArticleQuery productQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var articles = productQuery.GetLatestArticles();
        return View(articles);
    }
}
