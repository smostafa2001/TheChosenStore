using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAggregate
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        string GetSlug(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}