using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAggregate
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();
    }
}