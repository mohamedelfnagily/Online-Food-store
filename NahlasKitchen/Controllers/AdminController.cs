using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NahlasKitchen.Data;
using NahlasKitchen.EntityManager.ManageProduct;
using NahlasKitchen.EntityManager.ManageUser;
using NahlasKitchen.Models;

namespace NahlasKitchen.Controllers
{
    public class AdminController : Controller
    {
        AppdbContext db = new AppdbContext();
        private readonly IHttpContextAccessor mySessionContext;
        IManageProduct manageProduct = new ManageProduct();
        IManageUser manageUser = new ManageUser();
        public AdminController(IHttpContextAccessor myHttpContextAccessor, IManageProduct _manageProduct , IManageUser _manageUser)
        {
            mySessionContext = myHttpContextAccessor;
           manageProduct = _manageProduct;
            manageUser = _manageUser;
        }
        public IActionResult Index()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            var allPrpducts = db.Products.ToList();
            return View(allPrpducts);
        }

        public IActionResult AdminProducts()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            var myPRoducts = db.Products.Include(e => e.Category).ToList();
            return View(myPRoducts);
        }

        public IActionResult AdminUsers()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            var myUsers = db.Users.Where(e=>e.Role!="Admin").ToList();
            return View(myUsers);
        }
        public IActionResult AdminAdmins()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            var myAdmins = db.Users.Where(e => e.Role == "Admin").ToList();
            return View(myAdmins);
        }

        //Editing the product
        [HttpGet]
        public IActionResult EditProd(int id)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            ViewBag.cats = db.Categories.ToList();
            var myProduct = manageProduct.GetProductById(id);
            return View(myProduct);
        }
        [HttpPost]
        public IActionResult EditProd(Product prod)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            ViewBag.cats = db.Categories.ToList();
            manageProduct.EditProduct(prod);
            return RedirectToAction("AdminProducts");
        }

        //Adding new product
        [HttpGet]
        public IActionResult AddProduct()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            ViewBag.cats = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p, IFormFile Image1, IFormFile Image2 , IFormFile Image3)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            manageProduct.AddProduct(p, Image1, Image2, Image3);
            return RedirectToAction("AdminProducts");
        }

        //Deleting Existing Product
        public IActionResult DeleteProd(int id)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            manageProduct.DeleteProduct(id);
            return RedirectToAction("AdminProducts");
        }


        //Adding new admin
        [HttpGet]
        public IActionResult AddAdmin()
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddAdmin(User user)
        {
            user.Role = "Admin";
            manageUser.addUser(user);
            return RedirectToAction("AdminAdmins");
        }

        //Blocking a user
        public IActionResult BlockUser(int id)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }
            manageUser.deleteUser(id);
            return RedirectToAction("AdminUsers");
        }

        //Editting Admin
        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            //Checking the admin role
            var userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var myUser = db.Users.FirstOrDefault(e => e.Id == userId);
            if (myUser.Role != "Admin")
            {
                return BadRequest();
            }

            var userToUpdate = manageUser.getUserById(id);
            return View(userToUpdate);
        }
        [HttpPost]
        public IActionResult EditAdmin(User user)
        {
            manageUser.updateUser(user);
            return RedirectToAction("AdminAdmins");
        }

    }
}
