using ShoppingCart_ASP.NET_MVC5.Dao;
using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class PurchaseController : Controller
    {
        public ActionResult Purchase(string customer_id)
        {
            List<AllItemList> returnListofitems = PurchaseHistory.allhistory(customer_id);
            ViewBag.Products = returnListofitems;
            return View();
        }
    }
}