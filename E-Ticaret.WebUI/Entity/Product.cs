using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Entity
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Image  { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Stock { get; set; }
        public bool IsApproved { get; set; }
        public DateTime SaveDate { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
