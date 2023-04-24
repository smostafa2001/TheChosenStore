using AccountManagement.Application.Contracts.AccountAggregate;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderApplication _application;
        private readonly IAccountApplication _accountApplication;
        [TempData]
        public string Message { get; set; }

        public OrderSearchModel SearchModel { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public SelectList Accounts { get; set; }

        public IndexModel(IOrderApplication application, IAccountApplication accountApplication)
        {
            _application = application;
            _accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(_accountApplication.Accounts, "Id", "Fullname");
            Orders = _application.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            _application.PayOff(id, 0);
            Message = OperationMessages.IsSucceeded;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetCancel(long id)
        {
            _application.Cancel(id);
            Message = OperationMessages.IsSucceeded;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetItems(long id)
        {
            var items = _application.GetItems(id);
            return Partial("Items", items);
        }
    }
}