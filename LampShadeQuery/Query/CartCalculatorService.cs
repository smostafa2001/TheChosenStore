using DiscountManagement.Infrastructure.EFCore;
using Framework.Application;
using LampShadeQuery.Contracts.CartAggregate;
using ShopManagement.Application.Contracts.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountDbContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartCalculatorService(DiscountDbContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts.Where(cd => !cd.IsRemoved).Select(cd => new { cd.DiscountRate, cd.ProductId }).ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts.Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now).Select(cd => new { cd.DiscountRate, cd.ProductId }).ToList();
            string currentAccountRole = _authHelper.CurrentAccountRole();
            foreach (CartItem cartItem in cartItems)
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
}
