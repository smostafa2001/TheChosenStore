using Common.Application;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Domain.InventoryAggregate;
using System.Collections.Generic;

namespace InventoryManagement.Application.Implementations;

public class InventoryApplication(IInventoryRepository repository, IAuthHelper authHelper) : IInventoryApplication
{
    public OperationResult Create(CreateInventory command)
    {
        OperationResult operation = new OperationResult();
        if (repository.Exists(i => i.ProductId == command.ProductId))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        Inventory inventory = new Inventory(command.ProductId, command.UnitPrice);
        repository.Create(inventory);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditInventory command)
    {
        OperationResult operation = new OperationResult();
        Inventory inventory = repository.Get(command.Id);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (repository.Exists(i => i.ProductId == command.ProductId && i.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        inventory.Edit(command.ProductId, command.UnitPrice);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Increase(IncreaseInventory command)
    {
        OperationResult operation = new OperationResult();
        Inventory inventory = repository.Get(command.InventoryId);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        const long OperatorId = 1;
        inventory.IncreaseStock(command.Count, OperatorId, command.Description);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Decrease(DecreaseInventory command)
    {
        OperationResult operation = new OperationResult();
        Inventory inventory = repository.Get(command.InventoryId);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        var operatorId = authHelper.CurrentAccountId;
        inventory.DecreaseStock(command.Count, operatorId, command.Description, 0);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Decrease(List<DecreaseInventory> command)
    {
        OperationResult operation = new OperationResult();
        var operatorId = authHelper.CurrentAccountId;
        foreach (var item in command)
        {
            Inventory inventory = repository.GetInventory(item.ProductId);
            inventory.DecreaseStock(item.Count, operatorId, item.Description, item.OrderId);
        }

        repository.Save();
        return operation.Succeeded();
    }

    public EditInventory GetDetails(long id) => repository.GetDetails(id);
    public List<InventoryViewModel> Search(InventorySearchModel searchModel) => repository.Search(searchModel);
    public List<InventoryOperationViewModel> GetOperationLog(long inventoryId) => repository.GetOperationLog(inventoryId);
}
