using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShopManagement.Presentation.API;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductQuery productQuery) : ControllerBase
{
    [HttpGet]
    public List<ProductQueryModel> GetLatestArrivals() => productQuery.GetLatestArrivals();
}
