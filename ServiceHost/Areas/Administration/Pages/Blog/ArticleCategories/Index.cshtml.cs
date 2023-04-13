using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _application;

        [TempData]
        public string Message { get; set; }

        public ArticleCategorySearchModel SearchModel { get; set; }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }

        public IndexModel(IArticleCategoryApplication application) => _application = application;

        public void OnGet(ArticleCategorySearchModel searchModel) => ArticleCategories = _application.Search(searchModel);

        public IActionResult OnGetCreate() => Partial("./Create", new CreateArticleCategory());

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _application.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id) => Partial("./Edit", _application.GetDetails(id));

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _application.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetMore(long id)
        {
            var model = _application.GetFullDescription(id);
            return Partial("More", model);
        }
    }
}