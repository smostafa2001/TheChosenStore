using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;

namespace Host.Areas.Administration.Pages.Discounts.CustomerDiscounts;

public class IndexModel(IProductApplication productApplication, ICustomerDiscountApplication discountApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public CustomerDiscountSearchModel SearchModel { get; set; } = new();
    public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; } = [];
    public SelectList? Products { get; set; }

    public void OnGet(CustomerDiscountSearchModel searchModel)
    {
        Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
        CustomerDiscounts = discountApplication.Search(searchModel);
    }

    public IActionResult OnGetDefine()
    {
        var command = new DefineCustomerDiscount
        {
            Products = productApplication.GetProducts()
        };
        return Partial("./Define", command);
    }

    public JsonResult OnPostDefine(DefineCustomerDiscount command)
    {
        var result = discountApplication.Define(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var discount = discountApplication.GetDetails(id);
        discount.Products = productApplication.GetProducts();
        return Partial("./Edit", discount);
    }

    public JsonResult OnPostEdit(EditCustomerDiscount command)
    {
        var result = discountApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetMore(long id)
    {
        var model = discountApplication.GetFullReason(id);
        return Partial("More", model);
    }
}