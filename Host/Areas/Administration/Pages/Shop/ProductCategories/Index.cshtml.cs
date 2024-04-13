using Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace Host.Areas.Administration.Pages.Shop.ProductCategories;

public class IndexModel(IProductCategoryApplication application) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public ProductCategorySearchModel SearchModel { get; set; } = new();
    public List<ProductCategoryViewModel> ProductCategories { get; set; } = [];

    [NeedsPermission(ShopPermissions.ListProductCategories)]
    public void OnGet(ProductCategorySearchModel searchModel) => ProductCategories = application.Search(searchModel);

    public IActionResult OnGetCreate() => Partial("./Create", new CreateProductCategory());

    [NeedsPermission(ShopPermissions.CreateProductCategory)]
    public JsonResult OnPostCreate(CreateProductCategory command)
    {
        var result = application.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id) => Partial("./Edit", application.GetDetails(id));

    [NeedsPermission(ShopPermissions.EditProductCategory)]
    public JsonResult OnPostEdit(EditProductCategory command)
    {
        var result = application.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }
}