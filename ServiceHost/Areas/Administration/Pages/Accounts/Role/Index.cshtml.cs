using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        [TempData]
        public string Message { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public IndexModel(IRoleApplication roleApplication) => _roleApplication = roleApplication;

        public void OnGet() => Roles = _roleApplication.GetRoles();

        public IActionResult OnGetCreate()
        {
            var command = new CreateRole();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _roleApplication.GetDetails(id);
            return Partial("./Edit", account);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }
    }
}