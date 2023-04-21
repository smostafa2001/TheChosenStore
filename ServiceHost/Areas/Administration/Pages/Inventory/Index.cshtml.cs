using Framework.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Infrastructure.Configuration.Permissions;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;
        [TempData]
        public string Message { get; set; }

        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }
        [NeedsPermission(InventoryPermissions.ListInventory)]
        public void OnGet(InventorySearchModel searchModel) 
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("./Edit", inventory);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            IncreaseInventory command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Increase", command);
        }
        [NeedsPermission(InventoryPermissions.Increase)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetDecrease(long id)
        {
            DecreaseInventory command = new DecreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Decrease", command);
        }

        [NeedsPermission(InventoryPermissions.Decrease)]
        public JsonResult OnPostDecrease(DecreaseInventory command)
        {
            var result = _inventoryApplication.Decrease(command);
            Message = result.Message;
            return new JsonResult(result);
        }
        [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}