using ETicaret.WebUI.Entity;
using ETicaret.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{
    public class EfOrderRepository:EfGenericRepository<Order>,IOrderRepository
    {
        public EfOrderRepository(ETicaretContext context) : base(context)
        {

        }
        public ETicaretContext EContext
        {
            get { return context as ETicaretContext; }
        }
    }
}
