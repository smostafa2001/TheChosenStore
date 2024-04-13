using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Areas.Administration.Pages.Accounts.Role;

public class IndexModel(IRoleApplication roleApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public List<RoleViewModel> Roles { get; set; } = [];

    public void OnGet() => Roles = roleApplication.GetRoles();
}