using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Areas.Administration.Pages.Blog.Articles;

public class EditModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication) : PageModel
{
    public SelectList? ArticleCategories { get; set; }
    public EditArticle Command { get; set; } = new();
    public void OnGet(long id)
    {
        Command = articleApplication.GetDetails(id);
        ArticleCategories = new SelectList(categoryApplication.GetArticleCategories(), "Id", "Name");
    }

    public IActionResult OnPost(EditArticle command)
    {
        var result = articleApplication.Edit(command);
        TempData["OperationMessage"] = result.Message;
        return RedirectToPage("./Index");
    }
}