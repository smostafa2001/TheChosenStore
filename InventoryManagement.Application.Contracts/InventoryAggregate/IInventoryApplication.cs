﻿using Common.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.InventoryAggregate;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);

    OperationResult Edit(EditInventory command);

    OperationResult Increase(IncreaseInventory command);

    OperationResult Decrease(DecreaseInventory command);

    OperationResult Decrease(List<DecreaseInventory> command);

    EditInventory GetDetails(long id);

    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
}