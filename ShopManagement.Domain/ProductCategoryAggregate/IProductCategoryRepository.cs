using _01.Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAggregate
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        ProductCategory GetDetails(long id);
        List<ProductCategory> Search(string name);
    }
}
