using Framework.Application;
using Framework.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Domain.InventoryAggregate;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : BaseRepository<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryDbContext _inventoryContext;
        private readonly ShopDbContext _shopContext;

        public InventoryRepository(InventoryDbContext context, ShopDbContext shopContext) : base(context)
        {
            _inventoryContext = context;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(long id) => _inventoryContext.Inventory.Select(i => new EditInventory
        {
            Id = i.Id,
            ProductId = i.ProductId,
            UnitPrice = i.UnitPrice,
        }).FirstOrDefault(i => i.Id == id);

        public Inventory GetInventory(long productId) => _inventoryContext.Inventory.FirstOrDefault(i => i.ProductId == productId);
        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            Inventory inventory = _inventoryContext.Inventory.FirstOrDefault(i => i.Id == inventoryId);
            return inventory.Operations.Select(o => new InventoryOperationViewModel
            {
                Id = o.Id,
                Count = o.Count,
                CurrentStock = o.CurrentStock,
                Description = o.Description,
                Operation = o.Operation,
                OperationDate = o.OperationDate.ToFarsi(),
                OperatorId = o.OperatorId,
                Operator = "مدیر سیستم",
                OrderId = o.OrderId
            }).OrderByDescending(io => io.Id).ToList();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
            var query = _inventoryContext.Inventory.Select(i => new InventoryViewModel
            {
                Id = i.Id,
                UnitPrice = i.UnitPrice,
                IsInStock = i.IsInStock,
                ProductId = i.ProductId,
                CurrentStock = i.CalculateCurrentStock(),
                CreationDate = i.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(i => i.ProductId == searchModel.ProductId);

            if (searchModel.IsNotInStock)
                query = query.Where(i => !i.IsInStock);

            var inventory = query.OrderByDescending(i => i.Id).ToList();
            inventory.ForEach(item => { item.Product = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name; });
            return inventory;
        }
    }
}