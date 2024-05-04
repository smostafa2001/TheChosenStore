namespace TheChosenStoreQuery.Contracts.InventoryAggregate;

public interface IInventoryQuery
{
    StockStatus CheckStock(IsInStock command);
}
