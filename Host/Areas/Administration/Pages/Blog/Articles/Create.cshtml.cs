using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Areas.Administration.Pages.Blog.Articles;

public class CreateModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication) : PageModel
{
    public SelectList? ArticleCategories { get; set; }
    public CreateArticle Command { get; set; } = new();
    public void OnGet() => ArticleCategories = new SelectList(categoryApplication.GetArticleCategories(), "Id", "Name");

    public IActionResult OnPost(CreateArticle command)
    {
        var result = articleApplication.Create(command);
        TempData["OperationMessage"] = result.Message;
        return RedirectToPage("./Index");
    }
}
