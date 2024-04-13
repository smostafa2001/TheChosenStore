using Common.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAggregate;

public class Order(long accountId, int paymentMethod, double totalAmount, double discountAmount, double payableAmount) : EntityBase
{
    public long AccountId { get; private set; } = accountId;
    public int PaymentMethod { get; private set; } = paymentMethod;
    public double TotalAmount { get; private set; } = totalAmount;
    public double DiscountAmount { get; private set; } = discountAmount;
    public double PayableAmount { get; private set; } = payableAmount;
    public bool IsPaid { get; private set; } = false;
    public bool IsCanceled { get; private set; } = false;
    public string IssueTrackingNo { get; private set; }
    public long RefId { get; private set; } = 0;
    public List<OrderItem> Items { get; private set; } = [];

    public void PayOff(long refId)
    {
        IsPaid = true;
        if (refId != 0) RefId = refId;
    }
    public void Cancel() => IsCanceled = true;
    public void SetIssueTrackingNo(string number) => IssueTrackingNo = number;
    public void Add(OrderItem item) => Items.Add(item);
}
