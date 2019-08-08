using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.WebUI.Entity;
using ETicaret.WebUI.IdentityCore;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Repository.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;

namespace ETicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManager;
        public HomeController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signinManager)
        {
            uow = _uow;
            userManager = _userManager;
            signinManager = _signinManager;


        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Categories(int id)
        {

            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            var products = uow.Products.GetAll().Where(i => i.CategoryId == id);

            return View(new CategoryProductView { Products=products, Category=uow.Categories.Get(id)});


        }

        public IActionResult Index()
        {


            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            var products = uow.Products.GetAll().ToList();

            var promotions = uow.Promotions.GetAll().Where(i => i.StartingDate <= DateTime.Now).Where(i => i.EndDate >= DateTime.Now).ToList();

            foreach (var promo in promotions)
            {
                foreach (var produ in products)
                {
                    if (promo.CategoryId == produ.CategoryId)
                    {

                        var change = uow.Products.Get(produ.ProductId);

                        change.SalePrice = change.Price - change.Price *Convert.ToDecimal( promo.Sale) / 100;

                        uow.Products.Edit(change);
                        uow.SaveChanges();
                    }
                    if (promo.ProductId == produ.ProductId)
                    {
                        var change = uow.Products.Get(produ.ProductId);

                        change.SalePrice = change.Price - change.Price * Convert.ToDecimal(promo.Sale) / 100;

                        uow.Products.Edit(change);
                        uow.SaveChanges();
                    }
                }
            }
            
            return View(uow.Products.GetAll().Where(i => i.IsApproved == true).Take(20).ToList());
        }

        [HttpPost]
        public async Task<JsonResult> Login(string UserName,string Password)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user != null)
            {
                var result = await signinManager.PasswordSignInAsync(user, Password, false, false);
                if (result.Succeeded)
                {
                    return Json("login");
                }
                else
                {
                    return Json("hata");
                }
            }
            else
            {
                return Json("hata");
            }


        }

        [HttpGet]
        public IActionResult ContactUs()
        {

            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            return View();


        }

        [HttpPost]
        public RedirectResult ContactUs(string email,string name,string message,string username)
        {

            if (User.Identity.IsAuthenticated)
            {
                var messages = new MimeMessage();
                messages.From.Add(new MailboxAddress("Kayıtlı Kullanıcı", "foreveronehfttf@gmail.com"));
                messages.To.Add(new MailboxAddress("mail gonder", "foreveronehfttf@gmail.com"));
                messages.Subject = username;
                messages.Body = new TextPart("plain")
                {
                    Text = message
                };
                using(var client=new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("foreveronehfttf@gmail.com", "123456Fb.");
                    client.Send(messages);
                    client.Disconnect(true);
                    Redirect("/");
                }
            }
            else
            {
                var messages = new MimeMessage();
                messages.From.Add(new MailboxAddress("Ziyaretçi", "foreveronehfttf@gmail.com"));
                messages.To.Add(new MailboxAddress("mail gonder", "foreveronehfttf@gmail.com"));
                messages.Subject = name + " " + email;
                messages.Body = new TextPart("plain")
                {
                    Text = message
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("foreveronehfttf@gmail.com", "123456Fb.");
                    client.Send(messages);
                    client.Disconnect(true);
                    Redirect("/");
                }
            }

            return Redirect("/");

        }

        [HttpPost]
        public async Task<JsonResult> Register(UserReg u)
        {

            if(await userManager.FindByNameAsync(u.UserName) == null)
            {

                ApplicationUser user = new ApplicationUser()
                {
                    UserName=u.UserName,
                    Name=u.Name,
                    SurName=u.SurName,
                    Email=u.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, u.Password);
                if (result.Succeeded)
                {
                    return Json("tamam");
                }
                else
                {
                    return Json("hata");
                }
            }
            else
            {
                return Json("hata");
            }

        }

        public IActionResult ProductDetail(int id)
        {
            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);

            var proCat = uow.Products.Get(id).CategoryId;
            var products = uow.Products.GetAll().Where(i => i.CategoryId == proCat).Where(i => i.IsApproved == true).ToList().Take(3);

            ViewBag.sameProduct = products;
            return View(uow.Products.Get(id));

        }
        
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user =await userManager.GetUserAsync(User);
            ViewBag.Orders = uow.Orders.GetAll().Include(i=>i.Product).Where(i => i.UserName == User.Identity.Name).ToList();
            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(string username,string name,string surname,string email)
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.Orders = uow.Orders.GetAll().Include(i => i.Product).Where(i => i.UserName == User.Identity.Name).ToList();
            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            user.UserName = username;
            user.Name = name;
            user.SurName = surname;
            user.Email = email;

            var result =await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                Redirect("/");
            }

            ViewBag.user = user;
            return View();
        }

        public RedirectToActionResult Logout()
        {
            signinManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetCategories()
        {

            var result= JsonConvert.SerializeObject(uow.Categories.GetAll().ToList());
            return Json(result);

        }

        [HttpPost]
        public JsonResult GetSearch(string key)
        {

            var result = JsonConvert.SerializeObject(uow.Products.GetAll().Where(i=>i.ProductName.Contains(key)).ToList());

            return Json(result);


        }

        [HttpGet]
        public IActionResult checkOut()
        {

            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);

            return View();

        }

        [HttpPost]
        public JsonResult checkOut(Order o)
        {
            o.OrderDate = DateTime.Now;
            o.DeliveryDate = DateTime.Now.AddDays(3);
            o.Product = uow.Products.Get(o.ProductId);

            uow.Orders.Add(o);

            Product p = new Product();
            p = o.Product;

            int stock = p.Stock;
            int quantity = o.Quantity;


            int newStock=stock-quantity;
            p.Stock = newStock;
            uow.Products.Edit(p);

            uow.SaveChanges();

            return Json("tamam");

        }

        public RedirectResult Fav(int id)
        {
            Favorite f = new Favorite()
            {
                ProductId = id,
                UserName = User.Identity.Name
            };

            if (uow.Favorites.GetAll().Where(i => i.ProductId == id).Count() > 0)
            {

            }
            else { 
            uow.Favorites.Add(f);

            uow.SaveChanges();
            }
            return Redirect("/");
        }

        public IActionResult Favorites()
        {
            List<Product> p = new List<Product>();
            ViewBag.Sliders = uow.Sliders.GetAll().OrderByDescending(i => i.SliderId).Take(3);
            //ViewBag.Products = uow.Products.GetAll().Join(uow.Favorites.GetAll(), u => u.ProductId, a => a.ProductId, (u, a) => new { Product = u, Favorite = a }).ToList();
            foreach (var item in uow.Favorites.GetAll().ToList())
            {
                foreach (var item1 in uow.Products.GetAll().ToList())
                {
                    if (item.ProductId == item1.ProductId)
                    {
                        p.Add(item1);
                    }
                }
            }
            return View(p);
        }

        public RedirectResult RemoveFav(int id)
        {

            var entity = uow.Favorites.GetAll().Where(i => i.ProductId == id).FirstOrDefault();

            uow.Favorites.Delete(entity);
            uow.SaveChanges();

            return Redirect("/Home/Favorites");
        }

    }
}