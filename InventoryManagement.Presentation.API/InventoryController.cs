using InventoryManagement.Application.Contracts.InventoryAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheChosenStoreQuery.Contracts.InventoryAggregate;

namespace InventoryManagement.Presentation.API;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery)
{
    [HttpGet("{id}")]
    public List<InventoryOperationViewModel> GetOperation(long id) => inventoryApplication.GetOperationLog(id);
    [HttpPost]
    public StockStatus CheckStock(IsInStock command) => inventoryQuery.CheckStock(command);
}
