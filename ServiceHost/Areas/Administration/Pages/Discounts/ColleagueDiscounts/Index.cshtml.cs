using DiscountManagement.Application.Contracts;
using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _discountApplication;
        [TempData]
        public string Message { get; set; }

        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication discountApplication)
        {
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _discountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Define", command);
        }

        public JsonResult OnPostDefine(DefineColleagueDiscount command)
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

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _discountApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _discountApplication.Remove(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _discountApplication.Restore(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}