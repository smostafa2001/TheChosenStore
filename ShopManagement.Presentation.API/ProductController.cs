using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.ProductAggregate;

namespace ShopManagement.Presentation.API;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductQuery productQuery) : ControllerBase
{
    [HttpGet]
    public List<ProductQueryModel> GetLatestArrivals() => productQuery.GetLatestArrivals();
}
