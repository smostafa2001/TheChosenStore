using AccountManagement.Infrastructure.EFCore;
using Common.Application;
using Common.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Domain.InventoryAggregate;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository;

public class InventoryRepository(InventoryDbContext context, ShopDbContext shopContext, AccountDbContext accountContext) : BaseRepository<long, Inventory>(context), IInventoryRepository
{
    public EditInventory GetDetails(long id) => context.Inventory.Select(i => new EditInventory
    {
        Id = i.Id,
        ProductId = i.ProductId,
        UnitPrice = i.UnitPrice,
    }).FirstOrDefault(i => i.Id == id);

    public Inventory GetInventory(long productId) => context.Inventory.FirstOrDefault(i => i.ProductId == productId);
    public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
    {
        var accounts = accountContext.Accounts.Select(a => new { a.Id, a.Fullname }).ToList();
        Inventory inventory = context.Inventory.FirstOrDefault(i => i.Id == inventoryId);
        var operations = inventory.Operations.Select(o => new InventoryOperationViewModel
        {
            Id = o.Id,
            Count = o.Count,
            CurrentStock = o.CurrentStock,
            Description = o.Description,
            Operation = o.Operation,
            OperationDate = o.OperationDate.ToFarsi(),
            OperatorId = o.OperatorId,
            OrderId = o.OrderId
        }).OrderByDescending(io => io.Id).ToList();
        operations.ForEach(operation => operation.Operator = accounts.FirstOrDefault(a => a.Id == operation.OperatorId)?.Fullname);
        return operations;
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        var products = shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
        var query = context.Inventory.Select(i => new InventoryViewModel
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
        inventory.ForEach(item => item.Product = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name);
        return inventory;
    }
}