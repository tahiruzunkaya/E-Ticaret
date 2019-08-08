using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        IPromotionRepository Promotions { get; }

        ISliderRepository Sliders { get; }

        IOrderRepository Orders { get; }
        
        IFavoriteRepository Favorites { get; }


        int SaveChanges();
    }
}
