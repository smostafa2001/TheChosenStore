using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAggregate
{
    public interface IProductRepository : IRepository<long, Product>
    {
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
