using Microsoft.AspNetCore.Mvc;
using NahlasKitchen.Data;
using NahlasKitchen.EntityManager.ManageCart;
using NahlasKitchen.EntityManager.ManageUser;
using NahlasKitchen.Models.ViewModels;

namespace NahlasKitchen.Controllers
{
    public class CartController : Controller
    {
        AppdbContext db = new AppdbContext();
        private readonly IHttpContextAccessor mySessionContext;
        private IManageUser manageUser;
        private IManageCart manageCart;
        public CartController(IHttpContextAccessor myHttpContextAccessor, IManageUser _manageUser, IManageCart _manageCart)
        {
            mySessionContext = myHttpContextAccessor;
            manageUser = _manageUser;
            manageCart = _manageCart;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int prodId)
        {
            int id = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var carts = db.Carts.FirstOrDefault(e => e.userId == id);
            if(carts !=null)
            {
                manageCart.AddToCart(prodId,carts.Id);
            }
            var AllProds = manageCart.getUserProducts(id);
            CookieOptions op = new CookieOptions { Expires = DateTime.UtcNow.AddDays(3) };
            Response.Cookies.Append("ItemsInCart",AllProds.Count.ToString(),op);
            return RedirectToAction("Index","User");
        }

        //This action goeas to the view were all user products in cart are displayed
        public IActionResult DisplayCart()
        {
            CartViewModel ct = new CartViewModel();
            int userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var categoreis = db.Categories.Select(e => e).ToList();
            ViewBag.requiredData = new { cats = categoreis, sessionCheck = userId };
            var myProducts = manageCart.getUserProducts(userId);
            ct.prods = myProducts;
            ct.TotalPrice = manageCart.getTotalPrice(userId);
            return View(ct);
        }

        //This action is responsible for removing all items from the cart
        public IActionResult clearCart()
        {
            CookieOptions op = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-15) };
            Response.Cookies.Append("ItemsInCart", "", op);
            int userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            manageCart.clearCart(userId);
            return RedirectToAction("DisplayCart");
        }

        //Removing Item from the cart
        public IActionResult removeItem(int prodId)
        {
            var myProduct = db.Products.FirstOrDefault(e => e.Id==prodId);
            if (myProduct != null)
            {
                manageCart.RemoveFromCart(myProduct);
                int numberOfItems = int.Parse(Request.Cookies["ItemsInCart"]);
                numberOfItems -= 1;
                Response.Cookies.Append("ItemsInCart", numberOfItems.ToString());
            }
            return RedirectToAction("DisplayCart");
        }
    }
}
