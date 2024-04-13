using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class ProductCategoryWithProductsViewComponent(IProductCategoryQuery categoryQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var categories = categoryQuery.GetProductCategoriesWithProducts();
        return View(categories);
    }
}
