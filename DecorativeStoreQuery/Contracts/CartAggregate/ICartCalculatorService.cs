using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.CartAggregate;

public interface ICartCalculatorService
{
    Cart ComputeCart(List<CartItem> cartItems);
}
