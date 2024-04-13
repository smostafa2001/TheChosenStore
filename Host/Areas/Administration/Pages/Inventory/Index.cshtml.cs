using Common.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAggregate;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAggregate;

namespace Host.Areas.Administration.Pages.Inventory;

public class IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public InventorySearchModel SearchModel { get; set; } = new();
    public List<InventoryViewModel> Inventory { get; set; } = [];
    public SelectList? Products { get; set; }

    [NeedsPermission(InventoryPermissions.ListInventory)]
    public void OnGet(InventorySearchModel searchModel)
    {
        Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
        Inventory = inventoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new CreateInventory
        {
            Products = productApplication.GetProducts()
        };
        return Partial("./Create", command);
    }

    [NeedsPermission(InventoryPermissions.CreateInventory)]
    public JsonResult OnPostCreate(CreateInventory command)
    {
        var result = inventoryApplication.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var inventory = inventoryApplication.GetDetails(id);
        inventory.Products = productApplication.GetProducts();
        return Partial("./Edit", inventory);
    }

    [NeedsPermission(InventoryPermissions.EditInventory)]
    public JsonResult OnPostEdit(EditInventory command)
    {
        var result = inventoryApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetIncrease(long id)
    {
        var command = new IncreaseInventory
        {
            InventoryId = id
        };
        return Partial("./Increase", command);
    }

    [NeedsPermission(InventoryPermissions.Increase)]
    public JsonResult OnPostIncrease(IncreaseInventory command)
    {
        var result = inventoryApplication.Increase(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetDecrease(long id)
    {
        var command = new DecreaseInventory
        {
            InventoryId = id
        };
        return Partial("./Decrease", command);
    }

    [NeedsPermission(InventoryPermissions.Decrease)]
    public JsonResult OnPostDecrease(DecreaseInventory command)
    {
        var result = inventoryApplication.Decrease(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    [NeedsPermission(InventoryPermissions.OperationLog)]
    public IActionResult OnGetLog(long id)
    {
        var log = inventoryApplication.GetOperationLog(id);
        return Partial("OperationLog", log);
    }
}