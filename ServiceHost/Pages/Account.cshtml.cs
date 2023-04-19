using AccountManagement.Application.Contracts.AccountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        [TempData] public string Message { get; set; }
        public AccountModel(IAccountApplication accountApplication) => _accountApplication = accountApplication;

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSucceeded)
                RedirectToPage("/Index");

            Message = result.Message;
            return RedirectToPage("/Login");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
