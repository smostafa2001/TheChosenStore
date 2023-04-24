using LampShadeQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShopManagement.Infrastructure.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery) => _productQuery = productQuery;

        [HttpGet]
        public List<ProductQueryModel> GetLatestArrivals() => _productQuery.GetLatestArrivals();
    }
}
