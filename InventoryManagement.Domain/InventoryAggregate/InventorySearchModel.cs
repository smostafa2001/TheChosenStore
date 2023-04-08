namespace InventoryManagement.Domain.InventoryAggregate
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool IsNotInStock { get; set; }
    }
}