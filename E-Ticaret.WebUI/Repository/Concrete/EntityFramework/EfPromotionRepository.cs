using ETicaret.WebUI.Entity;
using ETicaret.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{
    public class EfPromotionRepository:EfGenericRepository<Promotion>,IPromotionRepository
    {
        public EfPromotionRepository(ETicaretContext context):base(context)
        {

        }

        public ETicaretContext Econtext
        {
            get { return context as ETicaretContext; }
        }

    }
}
