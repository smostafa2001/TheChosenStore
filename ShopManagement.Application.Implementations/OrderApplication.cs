using Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.OrderAggregate;
using ShopManagement.Domain.ACL;
using ShopManagement.Domain.OrderAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _repository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryACL _inventoryACL;
        public OrderApplication(IOrderRepository repository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryACL inventoryACL)
        {
            _repository = repository;
            _authHelper = authHelper;
            _configuration = configuration;
            _inventoryACL = inventoryACL;
        }

        public void Cancel(long id)
        {
            var order = _repository.Get(id);
            order.Cancel();
            _repository.Save();
        }

        public double GetAmount(long id) => _repository.GetAmount(id);
        public List<OrderItemViewModel> GetItems(long orderId) => _repository.GetItems(orderId);

        public string PayOff(long orderId, long refId)
        {
            var order = _repository.Get(orderId);
            order.PayOff(refId);
            var issueTrackingNo = CodeGenerator.Generate(_configuration["Symbol"]);
            order.SetIssueTrackingNo(issueTrackingNo);
            if (!_inventoryACL.DecreaseFromInventory(order.Items)) return null;
            _repository.Save();
            return issueTrackingNo;
        }

        public long PlaceOrder(Cart cart)
        {
            var order = new Order(_authHelper.CurrentAccountId, cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount, cart.PayableAmount);
            cart.Items.ForEach(item => order.Add(new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate)));
            _repository.Create(order);
            _repository.Save();
            return order.Id;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel) => _repository.Search(searchModel);
    }
}
