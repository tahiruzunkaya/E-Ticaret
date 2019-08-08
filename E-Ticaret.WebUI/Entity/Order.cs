using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
