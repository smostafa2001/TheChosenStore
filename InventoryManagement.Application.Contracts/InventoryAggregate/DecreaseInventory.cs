namespace InventoryManagement.Application.Contracts.InventoryAggregate
{
    public class DecreaseInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
    }
}