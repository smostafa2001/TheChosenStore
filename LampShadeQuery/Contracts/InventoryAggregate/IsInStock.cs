﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampShadeQuery.Contracts.InventoryAggregate
{
    public class IsInStock
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}
