using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Entity
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public string UserName { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
