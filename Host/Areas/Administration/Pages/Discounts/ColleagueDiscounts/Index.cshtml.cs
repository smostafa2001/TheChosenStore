using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;

namespace Host.Areas.Administration.Pages.Discounts.ColleagueDiscounts;

public class IndexModel(IProductApplication productApplication, IColleagueDiscountApplication discountApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public ColleagueDiscountSearchModel SearchModel { get; set; } = new();
    public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; } = [];
    public SelectList? Products { get; set; }

    public void OnGet(ColleagueDiscountSearchModel searchModel)
    {
        Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
        ColleagueDiscounts = discountApplication.Search(searchModel);
    }

    public IActionResult OnGetDefine()
    {
        var command = new DefineColleagueDiscount
        {
            Products = productApplication.GetProducts()
        };
        return Partial("./Define", command);
    }

    public JsonResult OnPostDefine(DefineColleagueDiscount command)
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

    public JsonResult OnPostEdit(EditColleagueDiscount command)
    {
        var result = discountApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        var result = discountApplication.Remove(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        var result = discountApplication.Restore(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }
}