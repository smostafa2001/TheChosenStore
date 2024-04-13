using Common.Domain;
using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAggregate;

public interface IOrderRepository : IRepository<long, Order>
{
    double GetAmount(long id);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
    List<OrderItemViewModel> GetItems(long orderId);
}
