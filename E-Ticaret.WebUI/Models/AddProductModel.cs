using ETicaret.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Models
{
    public class AddProductModel
    {
        public Product Product { get; set; }
        public int CategoryId { get; set; }
    }
}
