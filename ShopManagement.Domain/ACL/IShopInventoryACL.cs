using ShopManagement.Domain.OrderAggregate;
using System.Collections.Generic;

namespace ShopManagement.Domain.ACL
{
    public interface IShopInventoryACL
    {
        bool DecreaseFromInventory(List<OrderItem> items);
    }
}
