using Common.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.OrderAggregate;
using ShopManagement.Domain.ACL;
using ShopManagement.Domain.OrderAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations;

public class OrderApplication(IOrderRepository repository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryACL inventoryACL) : IOrderApplication
{
    public void Cancel(long id)
    {
        var order = repository.Get(id);
        order.Cancel();
        repository.Save();
    }

    public double GetAmount(long id) => repository.GetAmount(id);
    public List<OrderItemViewModel> GetItems(long orderId) => repository.GetItems(orderId);

    public string PayOff(long orderId, long refId)
    {
        var order = repository.Get(orderId);
        order.PayOff(refId);
        var issueTrackingNo = CodeGenerator.Generate(configuration["Symbol"]);
        order.SetIssueTrackingNo(issueTrackingNo);
        if (!inventoryACL.DecreaseFromInventory(order.Items)) return null;
        repository.Save();
        return issueTrackingNo;
    }

    public long PlaceOrder(Cart cart)
    {
        var order = new Order(authHelper.CurrentAccountId, cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount, cart.PayableAmount);
        cart.Items.ForEach(item => order.Add(new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate)));
        repository.Create(order);
        repository.Save();
        return order.Id;
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel) => repository.Search(searchModel);
}
