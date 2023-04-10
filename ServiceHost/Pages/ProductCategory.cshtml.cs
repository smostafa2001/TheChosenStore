using LampShadeQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryQueryModel ProductCategory { get; set; }

        public ProductCategoryModel(IProductCategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public void OnGet(string id)
        {
            ProductCategory = _categoryQuery.GetProductCategoryWithProducts(id);
        }
    }
}
