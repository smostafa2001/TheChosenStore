using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.ProductCategoryAggregate;

public interface IProductCategoryQuery
{
    ProductCategoryQueryModel GetProductCategoryWithProducts(string slug);
    List<ProductCategoryQueryModel> GetProductCategories();
    List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
}