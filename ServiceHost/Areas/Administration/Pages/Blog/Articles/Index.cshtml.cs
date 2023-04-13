using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        private readonly IArticleApplication _application;
        private readonly IArticleCategoryApplication _categoryApplication;

        [TempData]
        public string Message { get; set; }

        public ArticleSearchModel SearchModel { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public SelectList ArticleCategories { get; set; }

        public IndexModel(IArticleApplication application, IArticleCategoryApplication categoryApplication)
        {
            _application = application;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _application.Search(searchModel);
        }
        public IActionResult OnGetMore(long id)
        {
            var model = _application.GetFullShortDescription(id);
            return Partial("More", model);
        }
    }
}