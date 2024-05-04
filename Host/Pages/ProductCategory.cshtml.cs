using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChosenStoreQuery.Contracts.ProductCategoryAggregate;

namespace Host.Pages;

public class ProductCategoryModel(IProductCategoryQuery categoryQuery) : PageModel
{
    public ProductCategoryQueryModel? ProductCategory { get; set; }

    public void OnGet(string id) => ProductCategory = categoryQuery.GetProductCategoryWithProducts(id);
}
