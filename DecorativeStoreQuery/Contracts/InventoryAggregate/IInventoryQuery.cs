namespace DecorativeStoreQuery.Contracts.InventoryAggregate;

public interface IInventoryQuery
{
    StockStatus CheckStock(IsInStock command);
}
