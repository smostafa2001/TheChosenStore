using LampShadeQuery.Contracts.CartAggregate;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculator;

        public CheckoutModel(ICartCalculatorService cartCalculator) => _cartCalculator = cartCalculator;

        public Cart Cart { get; set; }
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            string value = Request.Cookies[CookieName];
            List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
            Cart = _cartCalculator.ComputeCart(cartItems);
        }
    }
}
