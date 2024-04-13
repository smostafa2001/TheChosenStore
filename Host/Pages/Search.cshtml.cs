using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Pages;

public class SearchModel(IProductQuery productQuery) : PageModel
{
    public string? Value { get; set; }

    public List<ProductQueryModel>? Products { get; set; }

    public void OnGet(string value)
    {
        Value = value;
        Products = productQuery.Search(value);
    }
}
