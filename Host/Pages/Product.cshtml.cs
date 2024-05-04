using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChosenStoreQuery.Contracts.ProductAggregate;

namespace Host.Pages;

public class ProductModel(IProductQuery productQuery, ICommentApplication commentApplication) : PageModel
{
    [TempData]
    public string? Message { get; set; }
    public ProductQueryModel? Product { get; set; }

    public void OnGet(string id) => Product = productQuery.GetDetails(id);

    public IActionResult OnPost(AddComment command, string productSlug)
    {
        command.Type = CommentType.Product;
        var result = commentApplication.Add(command);
        Message = result.Message;
        return RedirectToPage("/Product", new { Id = productSlug });
    }
}
