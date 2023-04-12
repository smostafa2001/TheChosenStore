using Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAggregate
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentStock()
        {
            var positives = Operations.Where(io => io.Operation).Sum(io => io.Count);
            var negatives = Operations.Where(io => !io.Operation).Sum(io => io.Count);
            return positives - negatives;
        }

        public void IncreaseStock(long count, long operatorId, string description)
        {
            var currentStock = CalculateCurrentStock() + count;
            var operation = new InventoryOperation(true, count, operatorId, currentStock, description, 0, Id);
            Operations.Add(operation);
            IsInStock = currentStock > 0;
        }

        public void DecreaseStock(long count, long operatorId, string description, long orderId)
        {
            var currentStock = CalculateCurrentStock() - count;
            var operation = new InventoryOperation(false, count, operatorId, currentStock, description, orderId, Id);
            Operations.Add(operation);
            IsInStock = currentStock > 0;
        }
    }
}