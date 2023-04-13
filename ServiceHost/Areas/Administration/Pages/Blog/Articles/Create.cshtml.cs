using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;

        public CreateModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication)
        {
            _categoryApplication = categoryApplication;
            _articleApplication = articleApplication;
        }

        public SelectList ArticleCategories { get; set; }
        public CreateArticle Command { get; set; }
        public void OnGet() => ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");

        public IActionResult OnPost(CreateArticle command)
        {
            var result = _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
