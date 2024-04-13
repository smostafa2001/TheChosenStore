using Common.Infrastructure;
using System.Collections.Generic;

namespace InventoryManagement.Infrastructure.Configuration.Permissions;

public class InventoryPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        {
            "Inventory", new List<PermissionDto>
            {
                new(InventoryPermissions.ListInventory, "ListInventory"),
                new(InventoryPermissions.SearchInventory, "SearchInventory"),
                new(InventoryPermissions.CreateInventory, "CreateInventory"),
                new(InventoryPermissions.EditInventory, "EditInventory"),
                new(InventoryPermissions.Increase, "Increase"),
                new(InventoryPermissions.Decrease, "Decrease"),
                new(InventoryPermissions.OperationLog, "OperationLog"),
            }
        }
    };
}
