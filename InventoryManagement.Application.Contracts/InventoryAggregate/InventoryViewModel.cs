namespace InventoryManagement.Application.Contracts.InventoryAggregate;

public class InventoryViewModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string Product { get; set; }
    public double UnitPrice { get; set; }
    public bool IsInStock { get; set; }
    public long CurrentStock { get; set; }
    public string CreationDate { get; set; }
}