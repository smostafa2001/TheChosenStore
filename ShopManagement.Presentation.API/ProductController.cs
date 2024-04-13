using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.API;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductQuery productQuery) : ControllerBase
{
    [HttpGet]
    public List<ProductQueryModel> GetLatestArrivals() => productQuery.GetLatestArrivals();
}
