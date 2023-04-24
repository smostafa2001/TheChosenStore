using Framework.Application;
using Framework.Application.ZarinPal;
using LampShadeQuery.Contracts.CartAggregate;
using LampShadeQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "cart-items";

        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculator;
        private readonly IZarinPalFactory _zarinPal;
        private readonly IAuthHelper _authHelper;

        public CheckoutModel(ICartCalculatorService cartCalculator, IProductQuery productQuery,
            ICartService cartService, IOrderApplication orderApplication, IZarinPalFactory zarinPal, IAuthHelper authHelper)
        {
            _cartCalculator = cartCalculator;
            _productQuery = productQuery;
            _cartService = cartService;
            _orderApplication = orderApplication;
            _zarinPal = zarinPal;
            _authHelper = authHelper;
        }

        public Cart Cart { get; set; }
        public void OnGet()
        {
            Cart = new Cart();
            var serializer = new JavaScriptSerializer();
            string value = Request.Cookies[CookieName];
            List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
            Cart = _cartCalculator.ComputeCart(cartItems);
            _cartService.Cart = Cart;
        }

        public IActionResult OnPostPayOff(int paymentMethod)
        {
            var cart = _cartService.Cart;
            cart.PaymentMethod = paymentMethod;
            var cartItems = _productQuery.CheckInventoryStatus(cart.Items);
            if (cartItems.Any(ci => !ci.IsInStock)) return RedirectToPage("/Cart");
            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPal.CreatePaymentRequest(cart.PayableAmount.ToString(), "", "", "خرید از درگاه لوازم خانگی و دکوری", orderId);
                return Redirect($"https://{_zarinPal.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            else
            {
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد؛ پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد", null));
            }
        }

        public IActionResult OnGetCallback([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmount(oId);
            var verificationResponse = _zarinPal.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));
            var result = new PaymentResult();
            if(status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PayOff(oId, verificationResponse.RefID);
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
}
