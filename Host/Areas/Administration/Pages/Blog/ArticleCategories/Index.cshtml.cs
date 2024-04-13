using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Areas.Administration.Pages.Blog.ArticleCategories;

public class IndexModel(IArticleCategoryApplication application) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public ArticleCategorySearchModel SearchModel { get; set; } = new();
    public List<ArticleCategoryViewModel> ArticleCategories { get; set; } = [];

    public void OnGet(ArticleCategorySearchModel searchModel) => ArticleCategories = application.Search(searchModel);

    public IActionResult OnGetCreate() => Partial("./Create", new CreateArticleCategory());

    public JsonResult OnPostCreate(CreateArticleCategory command)
    {
        var result = application.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id) => Partial("./Edit", application.GetDetails(id));

    public JsonResult OnPostEdit(EditArticleCategory command)
    {
        var result = application.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetMore(long id)
    {
        var model = application.GetFullDescription(id);
        return Partial("More", model);
    }
}