using ETicaret.WebUI.Entity;
using ETicaret.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{
    public class EfCategoryRepository: EfGenericRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(ETicaretContext context) : base(context)
        {

        }
        public ETicaretContext EContext
        {
            get { return context as ETicaretContext; }
        }
    }
}
