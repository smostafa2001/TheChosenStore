using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.ProductCategoryAggregate;

namespace Host.ViewComponents;

public class ProductCategoryWithProductsViewComponent(IProductCategoryQuery categoryQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var categories = categoryQuery.GetProductCategoriesWithProducts();
        return View(categories);
    }
}
