using Microsoft.AspNetCore.Mvc;
using ShopManagement.Domain.ProductCategoryAggregate;

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