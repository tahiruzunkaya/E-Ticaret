using ETicaret.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly ETicaretContext dbContext;

        public EfUnitOfWork(ETicaretContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("Db Context can not be null");

        }

        private ICategoryRepository _categories;
        private IProductRepository _products;
        private IPromotionRepository _promotions;
        private ISliderRepository _sliders;
        private IOrderRepository _orders;
        private IFavoriteRepository _favorites;

        public ICategoryRepository Categories
        {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(dbContext));
            }
        }

        public IPromotionRepository Promotions
        {
            get
            {
                return _promotions ?? (_promotions = new EfPromotionRepository(dbContext));
            }
        }

        public IProductRepository Products
        {
            get
            {
                return _products ?? (_products = new EfProductRepository(dbContext));
            }
        }

        public ISliderRepository Sliders
        {
            get
            {
                return _sliders ?? (_sliders = new EfSliderRepository(dbContext));
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                return _orders ?? (_orders = new EfOrderRepository(dbContext));
            }
        }
        public IFavoriteRepository Favorites
        {
            get
            {
                return _favorites ?? (_favorites = new EfFavoriteRepository(dbContext));
            }
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
