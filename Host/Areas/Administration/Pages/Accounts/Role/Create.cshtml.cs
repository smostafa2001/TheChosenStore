using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Areas.Administration.Pages.Accounts.Role;

public class CreateModel(IRoleApplication application) : PageModel
{
    public CreateRole Command { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost(CreateRole command)
    {
        var result = application.Create(command);
        TempData["OperationMessage"] = result.Message;
        return RedirectToPage("./Index");
    }
}
