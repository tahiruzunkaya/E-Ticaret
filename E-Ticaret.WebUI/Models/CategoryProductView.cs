using ETicaret.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Models
{
    public class CategoryProductView
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
