using Microsoft.AspNetCore.Mvc;
using NahlasKitchen.Data;
using NahlasKitchen.EntityManager.ManageCart;
using NahlasKitchen.EntityManager.ManageUser;
using NahlasKitchen.Models;

namespace NahlasKitchen.Controllers
{
    public class UserController : Controller
    {
        AppdbContext db = new AppdbContext();
        private readonly IHttpContextAccessor mySessionContext;
        private IManageUser manageUser;
        private IManageCart manageCart;
        public UserController(IHttpContextAccessor myHttpContextAccessor , IManageUser _manageUser , IManageCart _manageCart)
        {
            mySessionContext = myHttpContextAccessor;
            manageUser = _manageUser;
            manageCart = _manageCart;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["userEmail"] ==null && Request.Cookies["userPassword"] ==null)
            {
                if (mySessionContext.HttpContext.Session.GetString("UserId") == null)
                {
                    return RedirectToAction("SignIn");
                }
            }
            else
            {
                
                var myUser = db.Users.FirstOrDefault(e => e.Email == Request.Cookies["userEmail"]);
                mySessionContext.HttpContext.Session.SetString("UserId", myUser.Id.ToString());
                if(myUser.Role=="Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            int userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var allPrpducts = db.Products.ToList();
            var categoreis = db.Categories.Select(e => e).ToList();
            ViewBag.requiredData = new {cats= categoreis ,sessionCheck=userId};
            //ViewBag.user = db.Users.FirstOrDefault(e => e.Id == userId);
            return View(allPrpducts);
        }

        //Signin process
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User user,string rememberMe)
        {
            string userEmail = user.Email;
            string userPassword = manageUser.encryptPw(user.Password);
            var myUsers = db.Users.ToList();
            var targetUser = myUsers.FirstOrDefault(u => u.Email == userEmail && u.Password == userPassword);
            if(targetUser == null)
            {
                ViewBag.ValidateUser = "Invalid username and password";
                return View("SignIn");
            }
            else
            {
                mySessionContext.HttpContext.Session.SetString("UserId", targetUser.Id.ToString());
            }
            if(rememberMe=="true")
            {
                CookieOptions op = new CookieOptions { Expires = DateTime.UtcNow.AddDays(15) };
                Response.Cookies.Append("userEmail", targetUser.Email,op);
                Response.Cookies.Append("userPassword", targetUser.Password,op);
            }
            if(targetUser.Role=="Customer")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
            
        }

        //Sign in remote Validation
        public IActionResult checkEmail(string Email, int? Id , string Register)
        {
            //Sigining in and regestiration
            if(Id==null)
            {
                var myEmailUserCheck = db.Users.FirstOrDefault(e => e.Email == Email);
                if((myEmailUserCheck == null && Register!=null) || (myEmailUserCheck!=null&&Register==null))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            //Editing
            else
            {
                return Json(true);
            }
        }

        //Regestiration
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user , IFormFile pic)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            manageUser.addUser(user);
            manageCart.AssignCartToUser(user.Id);
            return RedirectToAction("SignIn");
        }


        //Logging user out
        public IActionResult logOut()
        {
            CookieOptions op = new CookieOptions{ Expires = DateTime.UtcNow.AddDays(-15) };
            Response.Cookies.Append("userEmail", "", op);
            Response.Cookies.Append("userPassword", "", op);
            mySessionContext.HttpContext.Session.Remove("UserId");
            return RedirectToAction("SignIn");
        }


        //ShowProductDetails
        public IActionResult showProductDetails(int id)
        {
            var myProduct = db.Products.FirstOrDefault(p => p.Id == id);
            ViewBag.prod = myProduct;
            return PartialView();
        }

        //Separate categoreis
        public IActionResult getSeperatedCategories(int Id)
        {
            if (Request.Cookies["userEmail"] == null && Request.Cookies["userPassword"] == null)
            {
                if (mySessionContext.HttpContext.Session.GetString("UserId") == null)
                {
                    return RedirectToAction("SignIn");
                }
            }
            var categoreis = db.Categories.Select(e => e).ToList();
            string userId = mySessionContext.HttpContext.Session.GetString("UserId");
            ViewBag.requiredData = new { cats = categoreis, sessionCheck = userId };
            var allProducts = db.Products.Where(e => e.CategoryId == Id).ToList();
            return View(allProducts);

        }

        //Pagenation
        //private List<Product> getNextList(string num)
        //{
        //    if(num!=null)
        //    {
        //        List<Product> myProducts;
        //        if(num == "-1")
        //        {
        //            myProducts = db.Products.ta
        //        }
        //        else if(num=="+1")
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //}
    }
}
