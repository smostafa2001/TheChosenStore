using InventoryManagement.Application.Contracts.InventoryAggregate;
using ShopManagement.Domain.ACL;
using ShopManagement.Domain.OrderAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.InventoryACL;

public class ShopInventoryACL(IInventoryApplication inventoryApplication) : IShopInventoryACL
{
    public bool DecreaseFromInventory(List<OrderItem> items)
    {
        var command = items.Select(orderItem => new DecreaseInventory
        {
            ProductId = orderItem.ProductId,
            Count = orderItem.Count,
            Description = "خرید مشتری",
            OrderId = orderItem.OrderId
        }).ToList();

        return inventoryApplication.Decrease(command).IsSucceeded;
    }
}
