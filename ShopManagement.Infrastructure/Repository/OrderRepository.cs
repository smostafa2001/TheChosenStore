using AccountManagement.Infrastructure.EFCore;
using Common.Application;
using Common.Infrastructure;
using ShopManagement.Application.Contracts.OrderAggregate;
using ShopManagement.Application.Implementations;
using ShopManagement.Domain.OrderAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class OrderRepository(ShopDbContext context, AccountDbContext accountContext) : BaseRepository<long, Order>(context), IOrderRepository
{
    public double GetAmount(long id)
    {
        var order = context.Orders.Select(o => new { o.Id, o.PayableAmount }).FirstOrDefault(o => o.Id == id);
        return order is not null ? order.PayableAmount : 0;
    }

    public List<OrderItemViewModel> GetItems(long orderId)
    {
        var products = context.Products.Select(p => new { p.Id, p.Name }).ToList();
        var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order is null) return [];
        var items = order.Items.Select(i => new OrderItemViewModel
        {
            Count = i.Count,
            DiscountRate = i.DiscountRate,
            Id = i.Id,
            OrderId = i.OrderId,
            ProductId = i.ProductId,
            UnitPrice = i.UnitPrice
        }).ToList();
        items.ForEach(item => item.Product = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name);
        return items;
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel)
    {
        var accounts = accountContext.Accounts.Select(a => new { a.Id, a.Fullname }).ToList();
        var query = context.Orders.Select(o => new OrderViewModel
        {
            Id = o.Id,
            AccountId = o.AccountId,
            DiscountAmount = o.DiscountAmount,
            IsCanceled = o.IsCanceled,
            IsPaid = o.IsPaid,
            IssueTrackingNo = o.IssueTrackingNo,
            PayableAmount = o.PayableAmount,
            PaymentMethodId = o.PaymentMethod,
            RefId = o.RefId,
            TotalAmount = o.TotalAmount,
            CreationDate = o.CreationDate.ToFarsi()
        });
        query = query.Where(o => o.IsCanceled == searchModel.IsCanceled);
        if (searchModel.AccountId > 0) query = query.Where(o => o.AccountId == searchModel.AccountId);
        var orders = query.OrderByDescending(o => o.Id).ToList();
        orders.ForEach(order =>
        {
            order.AccountFullName = accounts.FirstOrDefault(a => a.Id == order.AccountId)?.Fullname;
            order.PaymentMethodName = PaymentMethod.Get(order.PaymentMethodId).Name;
        });
        return orders;
    }
}
