using InventoryManagement.Application.Contracts.InventoryAggregate;
using ShopManagement.Domain.ACL;
using ShopManagement.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.InventoryACL
{
    public class ShopInventoryACL : IShopInventoryACL
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryACL(IInventoryApplication inventoryApplication) => _inventoryApplication = inventoryApplication;

        public bool DecreaseFromInventory(List<OrderItem> items)
        {
            var command = items.Select(orderItem => new DecreaseInventory
            {
                ProductId = orderItem.ProductId,
                Count = orderItem.Count,
                Description = "خرید مشتری",
                OrderId = orderItem.OrderId
            }).ToList();

            return _inventoryApplication.Decrease(command).IsSucceeded;
        }
    }
}
