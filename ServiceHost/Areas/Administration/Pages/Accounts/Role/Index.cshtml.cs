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
    }
}