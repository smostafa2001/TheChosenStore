using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductPictureAggregate;

namespace Host.Areas.Administration.Pages.Shop.ProductPictures;

public class IndexModel(IProductApplication productApplication, IProductPictureApplication pictureApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public ProductPictureSearchModel SearchModel { get; set; } = new();
    public List<ProductPictureViewModel> ProductPictures { get; set; } = [];
    public SelectList? Products { get; set; }

    public void OnGet(ProductPictureSearchModel searchModel)
    {
        Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
        ProductPictures = pictureApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new CreateProductPicture
        {
            Products = productApplication.GetProducts()
        };

        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(CreateProductPicture command)
    {
        var result = pictureApplication.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var productPicture = pictureApplication.GetDetails(id);
        productPicture.Products = productApplication.GetProducts();
        return Partial("./Edit", productPicture);
    }

    public JsonResult OnPostEdit(EditProductPicture command)
    {
        var result = pictureApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        var result = pictureApplication.Remove(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        var result = pictureApplication.Restore(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }
}