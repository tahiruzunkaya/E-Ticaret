using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.WebUI.Entity;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUnitOfWork uow;

        public AdminController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public IActionResult Index()
        {
            ViewBag.totalProduct = uow.Products.GetAll().Count();
            ViewBag.totalOrder = uow.Orders.GetAll().Count();
            ViewBag.totalPrice = uow.Orders.GetAll().Sum(i => i.TotalPrice);
            ViewBag.totalCategory = uow.Categories.GetAll().Count();
            ViewBag.lastOrder = uow.Orders.GetAll().Include(i=>i.Product).OrderByDescending(i => i.OrderDate).Take(10).ToList();
            

            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult AddCategory(Category entity)
        {
            if (ModelState.IsValid)
            {
                entity.SaveDate = DateTime.Now;
                uow.Categories.Add(entity);
                uow.SaveChanges();
                return Redirect("/Admin");
            }
            return Redirect("/Admin");
        }

        public IActionResult CategoryList()
        {

            var model = uow.Categories.GetAll().ToList();

            return View(model);

        }

        [HttpPost]
        public RedirectToActionResult DeleteCategory(int deleteId)
        {

            if (deleteId != 0)
            {
                var entity = uow.Categories.Get(deleteId);
                uow.Categories.Delete(entity);
                uow.SaveChanges();
                return RedirectToAction("CategoryList");
            }

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = uow.Categories.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product entity, IFormFile file, int categoryId)
        {
            if (ModelState.IsValid)
            {
                var category = uow.Categories.Get(categoryId);
                entity.Category = category;
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "node_modules\\images", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                        entity.Image = file.FileName;
                    }

                    uow.Products.Add(entity);
                    uow.SaveChanges();

                    return RedirectToAction("/");

                }
            }
            ViewBag.Categories = uow.Categories.GetAll().ToList();
            return View(entity);
        }

        public IActionResult ListProduct()
        {
            ViewBag.Categories = uow.Categories.GetAll().ToList();
            return View(uow.Products.GetAll().Include(i => i.Category));
        }

        [HttpPost]
        public JsonResult IsApproved(int productId, bool IsApproved)
        {

            var entity = uow.Products.Get(productId);

            entity.IsApproved = IsApproved;

            uow.Products.Edit(entity);

            uow.SaveChanges();

            return Json("");
        }

        [HttpGet]
        public JsonResult getProduct(int pid)
        {
            var result = JsonConvert.SerializeObject(uow.Products.Get(pid));
            return Json(result);

        }

        [HttpPost]
        public JsonResult AddProductPost(Product p)
        {

            var entity1 = uow.Products.Get(p.ProductId);

            var category = uow.Categories.Get(p.CategoryId);

            entity1.Category = category;
            entity1.Price = p.Price;
            entity1.ProductDescription = p.ProductDescription;
            entity1.ProductName = p.ProductName;
            entity1.SaveDate = DateTime.Now;
            entity1.Stock = p.Stock;



            uow.Products.Edit(entity1);
            uow.SaveChanges();
            return Json("");
        }


        
        public IActionResult SliderList()
        {

            return View(uow.Sliders.GetAll().ToList());

        }
        public IActionResult SliderDelete(int id)
        {
            var entity = uow.Sliders.Get(id);
            uow.Sliders.Delete(entity);
            uow.SaveChanges();
            return View("SliderList",uow.Sliders.GetAll().ToList());

        }

        [HttpPost]
        public JsonResult deleteProduct(int productId)
        {

            var entity = uow.Products.Get(productId);

            uow.Products.Delete(entity);

            uow.SaveChanges();
            return Json("tamam");

        }


        [HttpPost]
        public JsonResult AddPromotionCategory(Promotion p)
        {
            var entity = new Promotion();

            entity.Category = uow.Categories.Get(Convert.ToInt32(p.CategoryId));
            entity.Sale = p.Sale;
            entity.StartingDate = Convert.ToDateTime(p.StartingDate);
            entity.EndDate = Convert.ToDateTime(p.EndDate);
            entity.CategoryId = Convert.ToInt32(p.CategoryId);
            uow.Promotions.Add(entity);
            uow.SaveChanges();
            return Json("tamam");
        }

        [HttpPost]
        public JsonResult AddPromotionProduct(Promotion p)
        {
            var entity = new Promotion();
            entity.Product = uow.Products.Get(Convert.ToInt32(p.ProductId));
            entity.Sale = p.Sale;
            entity.StartingDate = Convert.ToDateTime(p.StartingDate);
            entity.EndDate = Convert.ToDateTime(p.EndDate);
            entity.ProductId = Convert.ToInt32(p.ProductId);

            uow.Promotions.Add(entity);
            uow.SaveChanges();

            return Json("tamam");

        }

        [HttpGet]
        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddSlider(Slider s, IFormFile file)
        {
            Slider sl = new Slider();
            sl = s;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "node_modules\\images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);

                sl.Image = file.FileName;

                uow.Sliders.Add(sl);
                uow.SaveChanges();
            }
            return Json("tamam");
        }

        public IActionResult OrderList()
        {


            return View(uow.Orders.GetAll().Include(i => i.Product).OrderByDescending(i => i.DeliveryDate).ToList());
        }
    }
}