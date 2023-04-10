using LampShadeQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value { get; set; }

        private readonly IProductQuery _productQuery;

        public List<ProductQueryModel> Products { get; set; }

        public SearchModel(IProductQuery productQuery) => _productQuery = productQuery;

        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);
        }
    }
}
