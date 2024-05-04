using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChosenStoreQuery.Contracts.ArticleAggregate;
using TheChosenStoreQuery.Contracts.ArticleCategoryAggregate;

namespace Host.Pages;

public class ArticleModel(
    IArticleQuery articleQuery,
    IArticleCategoryQuery categoryQuery,
    ICommentApplication commentApplication
    ) : PageModel
{
    [TempData] public string? Message { get; set; }
    public ArticleQueryModel? Article { get; set; }
    public List<ArticleQueryModel>? LatestArticles { get; set; }
    public List<ArticleCategoryQueryModel>? ArticleCategories { get; set; }

    public void OnGet(string id)
    {
        Article = articleQuery.GetArticleDetails(id);
        LatestArticles = articleQuery.GetLatestArticles();
        ArticleCategories = categoryQuery.GetArticleCategories();
    }

    public IActionResult OnPost(AddComment command, string articleSlug)
    {
        command.Type = CommentType.Article;
        var result = commentApplication.Add(command);
        Message = result.Message;
        return RedirectToPage("/Article", new { Id = articleSlug });
    }
}
