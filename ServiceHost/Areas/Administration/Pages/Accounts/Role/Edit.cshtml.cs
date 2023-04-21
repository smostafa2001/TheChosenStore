using AccountManagement.Application.Contracts.RoleAggregate;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _application;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public EditRole Command { get; set; }
        public List<SelectListItem> Permissions { get; set; } = new List<SelectListItem>();
        public EditModel(IRoleApplication application, IEnumerable<IPermissionExposer> exposers)
        {
            _application = application;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            Command = _application.GetDetails(id);
            var permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
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
            var result = _application.Edit(command);
            TempData["OperationMessage"] = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
