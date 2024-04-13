using Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace Host.Areas.Administration.Pages.Shop.Products;

public class IndexModel(IProductApplication productApplication, IProductCategoryApplication categoryApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public ProductSearchModel SearchModel { get; set; } = new();
    public List<ProductViewModel> Products { get; set; } = [];
    public SelectList? ProductCategories { get; set; }

    [NeedsPermission(ShopPermissions.ListProducts)]
    public void OnGet(ProductSearchModel searchModel)
    {
        ProductCategories = new SelectList(categoryApplication.GetProductCategories(), "Id", "Name");
        Products = productApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new CreateProduct
        {
            Categories = categoryApplication.GetProductCategories()
        };
        return Partial("./Create", command);
    }

    [NeedsPermission(ShopPermissions.CreateProduct)]
    public JsonResult OnPostCreate(CreateProduct command)
    {
        var result = productApplication.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var product = productApplication.GetDetails(id);
        product.Categories = categoryApplication.GetProductCategories();
        return Partial("./Edit", product);
    }

    [NeedsPermission(ShopPermissions.EditProduct)]
    public JsonResult OnPostEdit(EditProduct command)
    {
        var result = productApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }
}