using AccountManagement.Application.Contracts.AccountAggregate;
using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.OrderAggregate;

namespace Host.Areas.Administration.Pages.Shop.Orders;

public class IndexModel(IOrderApplication application, IAccountApplication accountApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public OrderSearchModel SearchModel { get; set; } = new();
    public List<OrderViewModel> Orders { get; set; } = [];
    public SelectList? Accounts { get; set; }

    public void OnGet(OrderSearchModel searchModel)
    {
        Accounts = new SelectList(accountApplication.Accounts, "Id", "Fullname");
        Orders = application.Search(searchModel);
    }

    public IActionResult OnGetConfirm(long id)
    {
        application.PayOff(id, 0);
        Message = OperationMessages.IsSucceeded;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetCancel(long id)
    {
        application.Cancel(id);
        Message = OperationMessages.IsSucceeded;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetItems(long id)
    {
        var items = application.GetItems(id);
        return Partial("Items", items);
    }
}