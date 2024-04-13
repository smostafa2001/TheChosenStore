namespace InventoryManagement.Application.Contracts.InventoryAggregate;

public class InventorySearchModel
{
    public long ProductId { get; set; }
    public bool IsNotInStock { get; set; }
}