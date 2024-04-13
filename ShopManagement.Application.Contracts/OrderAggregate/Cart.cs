using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.OrderAggregate;

public class Cart
{
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double PayableAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> Items { get; set; } = [];
    public void Add(CartItem cartItem)
    {
        Items.Add(cartItem);
        TotalAmount += cartItem.TotalItemPrice;
        DiscountAmount += cartItem.DiscountAmount;
        PayableAmount += cartItem.PayableAmount;
    }
}
