using InventoryManagement.Infrastructure.EFCore;
using LampShadeQuery.Contracts.InventoryAggregate;
using ShopManagement.Infrastructure.EFCore;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryDbContext _inventoryContext;
        private readonly ShopDbContext _shopContext;

        public InventoryQuery(InventoryDbContext inventoryContext, ShopDbContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(i => i.ProductId == command.ProductId);
            if (inventory is null || inventory.CalculateCurrentStock() < command.Count)
            {
                var product = _shopContext.Products.Select(p => new { p.Id, p.Name }).FirstOrDefault(p => p.Id == command.ProductId);
                return new StockStatus { IsInStock = false, ProductName = product?.Name };
            }

            return new StockStatus { IsInStock = true };
        }
    }
}
