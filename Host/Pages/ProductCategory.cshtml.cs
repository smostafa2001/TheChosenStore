using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Pages;

public class ProductCategoryModel(IProductCategoryQuery categoryQuery) : PageModel
{
    public ProductCategoryQueryModel? ProductCategory { get; set; }

    public void OnGet(string id) => ProductCategory = categoryQuery.GetProductCategoryWithProducts(id);
}
