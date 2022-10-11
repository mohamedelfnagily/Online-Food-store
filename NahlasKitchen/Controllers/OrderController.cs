using Microsoft.AspNetCore.Mvc;
using NahlasKitchen.Data;
using NahlasKitchen.EntityManager.ManageCart;
using NahlasKitchen.EntityManager.ManageUser;

namespace NahlasKitchen.Controllers
{
    public class OrderController : Controller
    {
        AppdbContext db = new AppdbContext();
        private readonly IHttpContextAccessor mySessionContext;
        private IManageUser manageUser;
        private IManageCart manageCart;
        public OrderController(IHttpContextAccessor myHttpContextAccessor, IManageUser _manageUser, IManageCart _manageCart)
        {
            mySessionContext = myHttpContextAccessor;
            manageUser = _manageUser;
            manageCart = _manageCart;
        }
        [HttpGet]
        public IActionResult OrderDetails()
        {
            int userId = int.Parse(mySessionContext.HttpContext.Session.GetString("UserId"));
            var categoreis = db.Categories.Select(e => e).ToList();
            ViewBag.requiredData = new { cats = categoreis, sessionCheck = userId };
            return View();
        }

    }
}
