using Common.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Host.Pages;

public class PaymentResultModel : PageModel
{
    public PaymentResult? Result { get; set; }
    public void OnGet(PaymentResult result) => Result = result;
}