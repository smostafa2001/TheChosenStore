using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.ProductCategoryAggregate;

namespace Host.ViewComponents;

public class ProductCategoryViewComponent(IProductCategoryQuery categoryQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var productCategories = categoryQuery.GetProductCategories();
        return View(productCategories);
    }
}