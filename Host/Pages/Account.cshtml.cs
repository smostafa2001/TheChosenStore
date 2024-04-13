using AccountManagement.Application.Contracts.AccountAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Pages;

public class AccountModel(IAccountApplication accountApplication) : PageModel
{
    [TempData]
    public string? Message { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPostLogin(Login command)
    {
        var result = accountApplication.Login(command);
        if (result.IsSucceeded)
            return RedirectToPage("/Index");

        Message = result.Message;
        return RedirectToPage("/Account");
    }

    public IActionResult OnGetLogout()
    {
        accountApplication.Logout();
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostRegister(RegisterAccount command)
    {
        var result = accountApplication.Register(command);
        Message = result.Message;
        return RedirectToPage("/Account");
    }
}
