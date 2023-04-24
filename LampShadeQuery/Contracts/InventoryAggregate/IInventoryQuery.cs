using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampShadeQuery.Contracts.InventoryAggregate
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(IsInStock command);
    }
}
