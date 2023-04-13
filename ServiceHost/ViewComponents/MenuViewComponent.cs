using LampShadeQuery.Contracts.ArticleCategoryAggregate;
using LampShadeQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Pages.Shared.Components.Menu;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery categoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = categoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View(model);
        }
    }
}
