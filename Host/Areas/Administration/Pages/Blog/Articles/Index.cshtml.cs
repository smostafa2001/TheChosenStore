using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Areas.Administration.Pages.Blog.Articles;

public class IndexModel(IArticleApplication application, IArticleCategoryApplication categoryApplication) : PageModel
{
    public ArticleSearchModel SearchModel { get; set; } = new();
    public List<ArticleViewModel> Articles { get; set; } = [];
    public SelectList? ArticleCategories { get; set; }

    public void OnGet(ArticleSearchModel searchModel)
    {
        ArticleCategories = new SelectList(categoryApplication.GetArticleCategories(), "Id", "Name");
        Articles = application.Search(searchModel);
    }
    public IActionResult OnGetMore(long id)
    {
        var model = application.GetFullShortDescription(id);
        return Partial("More", model);
    }
}