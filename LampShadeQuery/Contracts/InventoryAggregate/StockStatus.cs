using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampShadeQuery.Contracts.InventoryAggregate
{
    public class StockStatus
    {
        public bool IsInStock { get; set; }
        public string ProductName { get; set; }
    }
}
