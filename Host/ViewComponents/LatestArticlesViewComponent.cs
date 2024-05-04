using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.ArticleAggregate;

namespace Host.ViewComponents;

public class LatestArticlesViewComponent(IArticleQuery productQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var articles = productQuery.GetLatestArticles();
        return View(articles);
    }
}
