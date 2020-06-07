using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRepair.Model
{
    public class OrdersSpares
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int SpareId { get; set; }
        public Spare Spare { get; set; }
    }
}
