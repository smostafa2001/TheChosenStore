using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        [TempData]
        public string Message { get; set; }

        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public SelectList Roles { get; set; }

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.GetRoles(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.GetRoles()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetRoles();
            return Partial("./Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };
            return Partial("./ChangePassword", command);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            Message = result.Message;
            return new JsonResult(result);
        }
    }
}