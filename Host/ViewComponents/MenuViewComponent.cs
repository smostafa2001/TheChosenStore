using DecorativeStoreQuery.Contracts.ArticleCategoryAggregate;
using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;
using Host.Pages.Shared.Components.Menu;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class MenuViewComponent(IProductCategoryQuery categoryQuery, IArticleCategoryQuery articleCategoryQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var model = new MenuModel
        {
            ArticleCategories = articleCategoryQuery.GetArticleCategories(),
            ProductCategories = categoryQuery.GetProductCategories()
        };
        return View(model);
    }
}
