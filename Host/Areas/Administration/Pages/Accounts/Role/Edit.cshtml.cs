using AccountManagement.Application.Contracts.RoleAggregate;
using Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Host.Areas.Administration.Pages.Accounts.Role;

public class EditModel(IRoleApplication application, IEnumerable<IPermissionExposer> exposers) : PageModel
{
    public EditRole Command { get; set; } = new();
    public List<SelectListItem> Permissions { get; set; } = [];

    public void OnGet(long id)
    {
        Command = application.GetDetails(id);
        var permissions = new List<PermissionDto>();
        foreach (var exposer in exposers)
        {
            var exposedPermissions = exposer.Expose();
            foreach (var (key, value) in exposedPermissions)
            {
                permissions.AddRange(value);
                var group = new SelectListGroup { Name = key };
                foreach (var permission in value)
                {
                    var item = new SelectListItem(permission.Name, permission.Code.ToString()) { Group = group };
                    if (Command.MappedPermissions.All(p => p.Code == permission.Code))
                        item.Selected = true;

                    Permissions.Add(item);
                }
            }
        }
    }

    public IActionResult OnPost(EditRole command)
    {
        var result = application.Edit(command);
        TempData["OperationMessage"] = result.Message;
        return RedirectToPage("./Index");
    }
}
