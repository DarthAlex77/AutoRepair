using System.ComponentModel;

namespace AutoRepair.Model
{
    [Browsable(false)]
    public class OrderServices
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}