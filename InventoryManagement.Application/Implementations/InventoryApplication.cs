using InventoryManagement.Application.Contracts;
using InventoryManagement.Application.Shared;
using InventoryManagement.Domain.InventoryAggregate;
using System.Collections.Generic;

namespace InventoryManagement.Application.Implementations
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _repository;

        public InventoryApplication(IInventoryRepository repository) => _repository = repository;

        public OperationResult Create(CreateInventory command)
        {
            OperationResult operation = new OperationResult();
            if (_repository.DoesExist(i => i.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            Inventory inventory = new Inventory(command.ProductId, command.UnitPrice);
            _repository.Create(inventory);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            OperationResult operation = new OperationResult();
            Inventory inventory = _repository.Get(command.Id);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.DoesExist(i => i.ProductId == command.ProductId && i.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            OperationResult operation = new OperationResult();
            Inventory inventory = _repository.Get(command.InventoryId);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;
            inventory.IncreaseStock(command.Count, operatorId, command.Description);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Decrease(DecreaseInventory command)
        {
            OperationResult operation = new OperationResult();
            Inventory inventory = _repository.Get(command.InventoryId);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;
            inventory.DecreaseStock(command.Count, operatorId, command.Description, 0);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            OperationResult operation = new OperationResult();
            const long operatorId = 1;
            foreach (var item in command)
            {
                Inventory inventory = _repository.GetInventory(item.ProductId);
                inventory.DecreaseStock(item.Count, operatorId, item.Description, item.OrderId);
            }

            _repository.Save();
            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id) => _repository.GetDetails(id);
        public List<InventoryViewModel> Search(InventorySearchModel searchModel) => _repository.Search(searchModel);
        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId) => _repository.GetOperationLog(inventoryId);
    }
}
