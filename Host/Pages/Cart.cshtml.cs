using DecorativeStoreQuery.Contracts.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.OrderAggregate;

namespace Host.Pages;

public class CartModel(IProductQuery productQuery) : PageModel
{
    public const string CookieName = "cart-items";

    public List<CartItem> CartItems { get; set; } = [];
    public void OnGet()
    {
        var serializer = new JavaScriptSerializer();
        string value = Request?.Cookies[CookieName]!;
        List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
        CartItems = productQuery.CheckInventoryStatus(cartItems);
    }

    public IActionResult OnGetRemoveFromCart(long id)
    {
        var serializer = new JavaScriptSerializer();
        string value = Request?.Cookies[CookieName]!;
        Response.Cookies.Delete(CookieName);
        List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
        var itemToRemove = cartItems.FirstOrDefault(ci => ci.Id == id);
        cartItems.Remove(itemToRemove!);
        var options = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
        Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
        return RedirectToPage("/Cart");
    }

    public IActionResult OnGetGoToCheckOut()
    {
        var serializer = new JavaScriptSerializer();
        string value = Request?.Cookies[CookieName]!;
        List<CartItem> cartItems = serializer.Deserialize<List<CartItem>>(value);
        CartItems = productQuery.CheckInventoryStatus(cartItems);
        return CartItems.Any(a => !a.IsInStock) ? RedirectToPage("/Cart") : (IActionResult)RedirectToPage("/Checkout");
    }
}
