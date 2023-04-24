using Framework.Domain;
using ShopManagement.Application.Contracts.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAggregate
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        double GetAmount(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
    }
}
