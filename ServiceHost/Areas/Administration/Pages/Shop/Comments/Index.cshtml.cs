using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.CommentAggregate;
using ShopManagement.Domain.CommentAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;

        [TempData]
        public string Message { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public CommentSearchModel SearchModel { get; set; }

        public IndexModel(ICommentApplication commentApplication) => _commentApplication = commentApplication;

        public void OnGet(CommentSearchModel searchModel) => Comments = _commentApplication.Search(searchModel);

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _commentApplication.RemoveComment(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _commentApplication.Restore(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetReview(long id)
        {
            var result = _commentApplication.Review(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}