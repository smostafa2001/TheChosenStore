using LampShadeQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.CommentAggregate;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        [TempData] public string Message { get; set; }
        public ProductQueryModel Product { get; set; }

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            var result = _commentApplication.Add(command);
            Message = result.Message;
            return RedirectToPage("/Product", new { Id = productSlug });
        }
    }
}