using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Entity
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public double Sale { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
