namespace ShopManagement.Application.Contracts.OrderAggregate
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public double TotalItemPrice { get => UnitPrice * Count; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double PayableAmount { get; set; }
    }
}
