using LampShadeQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public IViewComponentResult Invoke()
        {
            var categories = _categoryQuery.GetProductCategoriesWithProducts();
            return View(categories);
        }
    }
}
