using Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAggregate
{
    public class Order : EntityBase
    {
        public long AccountId { get; private set; }
        public int PaymentMethod { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayableAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, int paymentMethod, double totalAmount, double discountAmount, double payableAmount)
        {
            AccountId = accountId;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayableAmount = payableAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void PayOff(long refId)
        {
            IsPaid = true;
            if (refId != 0) RefId = refId;
        }
        public void Cancel() => IsCanceled = true;
        public void SetIssueTrackingNo(string number) => IssueTrackingNo = number;
        public void Add(OrderItem item) => Items.Add(item);
    }
}
