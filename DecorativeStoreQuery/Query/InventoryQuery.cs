using DecorativeStoreQuery.Contracts.InventoryAggregate;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore;
using System.Linq;

namespace DecorativeStoreQuery.Query;

public class InventoryQuery(InventoryDbContext inventoryContext, ShopDbContext shopContext) : IInventoryQuery
{
    public StockStatus CheckStock(IsInStock command)
    {
        var inventory = inventoryContext.Inventory.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (inventory is null || inventory.CalculateCurrentStock() < command.Count)
        {
            var product = shopContext.Products.Select(p => new { p.Id, p.Name }).FirstOrDefault(p => p.Id == command.ProductId);
            return new StockStatus { IsInStock = false, ProductName = product!.Name };
        }

        return new StockStatus { IsInStock = true };
    }
}
