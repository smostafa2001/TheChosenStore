using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        private readonly IProductPictureApplication _pictureApplication;
        private readonly IProductApplication _productApplication;

        [TempData]
        public string Message { get; set; }

        public ProductPictureSearchModel SearchModel { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, IProductPictureApplication pictureApplication)
        {
            _productApplication = productApplication;
            _pictureApplication = pictureApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _pictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _pictureApplication.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _pictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("./Edit", productPicture);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _pictureApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _pictureApplication.Remove(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _pictureApplication.Restore(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}