using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _application;

        [TempData]
        public string Message { get; set; }

        public ProductCategorySearchModel SearchModel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }

        public IndexModel(IProductCategoryApplication application) => _application = application;

        public void OnGet(ProductCategorySearchModel searchModel) => ProductCategories = _application.Search(searchModel);

        public IActionResult OnGetCreate() => Partial("./Create", new CreateProductCategory());

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _application.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id) => Partial("./Edit", _application.GetDetails(id));

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _application.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }
    }
}