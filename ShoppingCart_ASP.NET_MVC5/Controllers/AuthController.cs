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
using System.Security.Cryptography;


namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    

    public class AuthController : Controller
    {
       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == null)
                return View();

            Customer customer = CustomerDetails.GetCustomer(username);
            if (customer == null)
            {
                ViewData["warning1"] = "Wrong Username, Please try again";
                return View();
            }

            if(customer.username.Equals(username))
            {
                if (customer.password != CalculateMD5Hash(password).ToLower())
                {
                    ViewData["warning"] = "Wrong password, Please try again";
                    return View();
                }
            }
            else
            {
                ViewData["warning"] = "Invalid username";
                return View();
            }

            HttpCookie cookie = new HttpCookie("customer_id");
            HttpCookie firstname = new HttpCookie("firstname");
            HttpCookie Count = new HttpCookie("Count");
            Count.Value = "0";
            cookie.Value = customer.customer_id.ToString();
            firstname.Value = customer.firstname;
            HttpContext.Response.Cookies.Add(cookie);
            HttpContext.Response.Cookies.Add(Count);
            HttpContext.Response.Cookies.Add(firstname);
            return RedirectToAction("../Home/Home");
        }

        //Logout
        public ActionResult Logout(string sessionId, string username)
        {
            SessionManagement.RemoveSession(sessionId);
            return View("Login");
        }


        public string CalculateMD5Hash(string input)

        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
