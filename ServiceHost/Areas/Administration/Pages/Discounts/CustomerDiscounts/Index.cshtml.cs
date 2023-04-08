using DiscountManagement.Application.Contracts;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _discountApplication;

        [TempData]
        public string Message { get; set; }

        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication discountApplication)
        {
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _discountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Define", command);
        }

        public JsonResult OnPostDefine(DefineCustomerDiscount command)
        {
            var result = _discountApplication.Define(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var discount = _discountApplication.GetDetails(id);
            discount.Products = _productApplication.GetProducts();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _discountApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }
    }
}