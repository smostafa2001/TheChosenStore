using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.ProductAggregate;

namespace Host.ViewComponents;

public class LatestArrivalsViewComponent(IProductQuery productQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var products = productQuery.GetLatestArrivals();
        return View(products);
    }
}
