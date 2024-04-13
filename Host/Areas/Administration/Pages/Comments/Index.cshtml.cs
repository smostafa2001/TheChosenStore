using CommentManagement.Application.Contracts.CommentAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Areas.Administration.Pages.Comments;

public class IndexModel(ICommentApplication commentApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public List<CommentViewModel> Comments { get; set; } = [];
    public CommentSearchModel SearchModel { get; set; } = new();

    public void OnGet(CommentSearchModel searchModel) => Comments = commentApplication.Search(searchModel);

    public IActionResult OnGetCancel(long id)
    {
        var result = commentApplication.Cancel(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetConfirm(long id)
    {
        var result = commentApplication.Confirm(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRemove(long id)
    {
        var result = commentApplication.RemoveComment(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        var result = commentApplication.Restore(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }
    public IActionResult OnGetReview(long id)
    {
        var result = commentApplication.Review(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetMore(long id)
    {
        var model = commentApplication.GetFullMessage(id);
        return Partial("More", model);
    }
}