using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class EditModel : PageModel
{
    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _categoryApplication;

    public EditModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication)
    {
        _categoryApplication = categoryApplication;
        _articleApplication = articleApplication;
    }

    public SelectList ArticleCategories { get; set; }
    public EditArticle Command { get; set; }
    public void OnGet(long id)
    {
        Command = _articleApplication.GetDetails(id);
        ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
    }

    public IActionResult OnPost(EditArticle command)
    {
        var result = _articleApplication.Edit(command);
        TempData["OperationMessage"] = result.Message;
        return RedirectToPage("./Index");
    }
}