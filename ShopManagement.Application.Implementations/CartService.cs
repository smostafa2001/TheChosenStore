using ShopManagement.Application.Contracts.OrderAggregate;

namespace ShopManagement.Application.Implementations
{
    public class CartService : ICartService
    {
        public Cart Cart { get; set; }
    }
}
