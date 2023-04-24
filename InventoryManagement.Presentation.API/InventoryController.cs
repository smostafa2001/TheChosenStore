using InventoryManagement.Application.Contracts.InventoryAggregate;
using LampShadeQuery.Contracts.InventoryAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagement.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IInventoryQuery _inventoryQuery;

        public InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery)
        {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }

        [HttpGet("{id}")]
        public List<InventoryOperationViewModel> GetOperation(long id) => _inventoryApplication.GetOperationLog(id);
        [HttpPost]
        public StockStatus CheckStock(IsInStock command)=>_inventoryQuery.CheckStock(command);
    }
}
