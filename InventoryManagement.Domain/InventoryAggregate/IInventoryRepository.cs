using Common.Domain;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAggregate;

public interface IInventoryRepository : IRepository<long, Inventory>
{
    EditInventory GetDetails(long id);

    Inventory GetInventory(long productId);

    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
}