using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.OrderAggregate;

public interface IOrderApplication
{
    long PlaceOrder(Cart cart);
    string PayOff(long orderId, long refId);
    double GetAmount(long id);
    void Cancel(long id);
    List<OrderItemViewModel> GetItems(long orderId);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
}
