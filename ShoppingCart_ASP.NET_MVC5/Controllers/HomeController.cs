using ShoppingCart_ASP.NET_MVC5.Dao;
using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    [SessionExpire]
    public class HomeController : Controller
    {
       
        public ActionResult Home()
        {
            List<Product> prolist = ProductDetails.callme();
            ViewData["pl"] = prolist;
            string sessionId = SessionManagement.CreateSession(int.Parse(Request.Cookies["customer_id"].Value));
            ViewData["sessionId"] = sessionId;
            ViewBag.a = 1;
            ViewBag.customer = int.Parse(Request.Cookies["customer_id"].Value);
            ViewBag.Count = int.Parse(Request.Cookies["Count"].Value);
            ViewBag.firstname = Request.Cookies["firstname"].Value;
            return View();

        }


        


    }
}