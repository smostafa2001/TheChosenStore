using Common.Application.ZarinPal;
using DecorativeStoreQuery.Contracts.CartAggregate;
using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.OrderAggregate;
using System.Globalization;

namespace Host.Pages;

[Authorize]
public class CheckoutModel(
    ICartCalculatorService cartCalculator, 
    IProductQuery productQuery,
    ICartService cartService, 
    IOrderApplication orderApplication,
    IZarinPalFactory zarinPal
    ) : PageModel
{
    public const string CookieName = "cart-items";

    public Cart Cart { get; set; } = new();
    public void OnGet()
    {
        Cart = new Cart();
        var serializer = new JavaScriptSerializer();
        string value = Request?.Cookies[CookieName]!;
        List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
        Cart = cartCalculator.ComputeCart(cartItems);
        cartService.Cart = Cart;
    }

    public IActionResult OnPostPayOff(int paymentMethod)
    {
        var cart = cartService.Cart;
        cart.PaymentMethod = paymentMethod;
        var cartItems = productQuery.CheckInventoryStatus(cart.Items);
        if (cartItems.Any(ci => !ci.IsInStock)) return RedirectToPage("/Cart");
        var orderId = orderApplication.PlaceOrder(cart);
        if (paymentMethod == 1)
        {
            var paymentResponse = zarinPal.CreatePaymentRequest(cart.PayableAmount.ToString(), "", "", "خرید از درگاه لوازم خانگی و دکوری", orderId);
            return Redirect($"https://{zarinPal.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }
        else
        {
            var paymentResult = new PaymentResult();
            return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد؛ پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد", null!));
        }
    }

    public IActionResult OnGetCallback([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
    {
        var orderAmount = orderApplication.GetAmount(oId);
        var verificationResponse = zarinPal.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));
        var result = new PaymentResult();
        if (status == "OK" && verificationResponse.Status >= 100)
        {
            var issueTrackingNo = orderApplication.PayOff(oId, verificationResponse.RefID);
            Response.Cookies.Delete("cart-items");
            result = result.Succeeded("پرداخت با موفقیت انجام شد", issueTrackingNo);
            return RedirectToPage("/PaymentResult", result);
        }
        else
        {

            result = result.Failed("پرداخت ناموفق بود؛ در صورت کسر وجه از حساب شما، مبلغ کسر شده تا ۲۴ ساعت آینده به حسابتان بازگردانده خواهد شد");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
