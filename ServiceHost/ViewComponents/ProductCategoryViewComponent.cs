using LampShadeQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public IViewComponentResult Invoke()
        {
            var productCategories = _categoryQuery.GetProductCategories();
            return View(productCategories);
        }
    }
}