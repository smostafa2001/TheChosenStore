using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class LatestArrivalsViewComponent(IProductQuery productQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var products = productQuery.GetLatestArrivals();
        return View(products);
    }
}
