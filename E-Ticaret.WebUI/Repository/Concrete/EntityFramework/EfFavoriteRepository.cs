using ETicaret.WebUI.Entity;
using ETicaret.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{
    public class EfFavoriteRepository:EfGenericRepository<Favorite>,IFavoriteRepository
    {
        public EfFavoriteRepository(ETicaretContext context) : base(context)
        {

        }
        public ETicaretContext EContext
        {
            get { return context as ETicaretContext; }
        }
    }
}
