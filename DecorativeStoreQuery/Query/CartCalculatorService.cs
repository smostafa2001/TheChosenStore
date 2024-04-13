using Common.Application;
using DecorativeStoreQuery.Contracts.CartAggregate;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecorativeStoreQuery.Query;

public class CartCalculatorService(DiscountDbContext discountContext, IAuthHelper authHelper) : ICartCalculatorService
{
    public Cart ComputeCart(List<CartItem> cartItems)
    {
        var cart = new Cart();
        var colleagueDiscounts = discountContext.ColleagueDiscounts.Where(cd => !cd.IsRemoved).Select(cd => new { cd.DiscountRate, cd.ProductId }).ToList();
        var customerDiscounts = discountContext.CustomerDiscounts.Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now).Select(cd => new { cd.DiscountRate, cd.ProductId }).ToList();
        string currentAccountRole = authHelper.CurrentAccountRole();
        foreach (var cartItem in cartItems)
        {
            if (currentAccountRole is "4")
            {
                var colleagueDiscount = colleagueDiscounts.FirstOrDefault(cd => cd.ProductId == cartItem.Id);
                if (colleagueDiscount is not null) cartItem.DiscountRate = colleagueDiscount.DiscountRate;

            }
            else
            {
                var customerDiscount = customerDiscounts.FirstOrDefault(cd => cd.ProductId == cartItem.Id);
                if (customerDiscount is not null) cartItem.DiscountRate = customerDiscount.DiscountRate;
            }

            cartItem.DiscountAmount = cartItem.TotalItemPrice * cartItem.DiscountRate / 100;
            cartItem.PayableAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
            cart.Add(cartItem);
        }

        return cart;
    }
}
