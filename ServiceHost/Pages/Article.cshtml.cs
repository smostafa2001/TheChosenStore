using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Infrastructure.EFCore;
using LampShadeQuery.Contracts.ArticleAggregate;
using LampShadeQuery.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;
        private readonly ICommentApplication _commentApplication;
        [TempData]public string Message { get; set; }
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _categoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);
            Message = result.Message;
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
