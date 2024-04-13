namespace ShopManagement.Application.Contracts.OrderAggregate;

public class OrderSearchModel
{
    public long AccountId { get; set; }
    public bool IsCanceled { get; set; }
}
