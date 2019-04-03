using ShoppingCart_ASP.NET_MVC5.Dao;
using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    

    public class AuthController : Controller
    {
        //Login
        public ActionResult Login()
        {
            Debug.WriteLine("Welcome to HttpGet Method");
            return View();
        }
        
        //Logout
        public ActionResult Logout(string sessionId)
        {
            SessionManagement.RemoveSession(sessionId);
            return View("Login");
        }

    }
}
