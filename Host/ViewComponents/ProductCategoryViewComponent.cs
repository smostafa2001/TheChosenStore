using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class ProductCategoryViewComponent(IProductCategoryQuery categoryQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var productCategories = categoryQuery.GetProductCategories();
        return View(productCategories);
    }
}