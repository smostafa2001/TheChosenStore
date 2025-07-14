using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Areas.Administration.Pages.Accounts.Account;

public class IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public AccountSearchModel SearchModel { get; set; } = new();
    public List<AccountViewModel> Accounts { get; set; } = [];
    public SelectList? Roles { get; set; }

    public void OnGet(AccountSearchModel searchModel)
    {
        Roles = new SelectList(roleApplication.GetRoles(), "Id", "Name");
        Accounts = accountApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new RegisterAccount
        {
            Roles = roleApplication.GetRoles()
        };

        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(RegisterAccount command)
    {
        var result = accountApplication.Register(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var account = accountApplication.GetDetails(id);
        account.Roles = roleApplication.GetRoles();
        return Partial("./Edit", account);
    }

    public JsonResult OnPostEdit(EditAccount command)
    {
        var result = accountApplication.Edit(command);
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
        var result = accountApplication.ChangePassword(command);
        Message = result.Message;
        return new JsonResult(result);
    }
}