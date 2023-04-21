using AccountManagement.Application.Contracts.RoleAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        private readonly IRoleApplication _application;
        public CreateRole Command { get; set; }
        public CreateModel(IRoleApplication application) => _application = application;

        public void OnGet() { }

        public IActionResult OnPost(CreateRole command)
        {
            var result = _application.Create(command);
            TempData["OperationMessage"] = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
