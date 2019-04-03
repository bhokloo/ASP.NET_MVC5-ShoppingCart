using ShoppingCart_ASP.NET_MVC5.Dao;
using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

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


        public ActionResult ViewCart()
        {
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys;
            Dictionary<string, string> kV = new Dictionary<string, string>();
            List<Product> pro = new List<Product>();
            foreach (var i in arr)
            {
                if (i.Length <= 3)
                {
                    kV.Add(i, Request.Cookies[i].Value);
                    using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
                    {
                        conn.Open();
                        string sql = @"select * from Product where pro_id = '" + i + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Product p = new Product()
                            {
                                pro_id = (int)reader["pro_id"],
                                pro_name = (string)reader["pro_name"],
                                pro_desc = (string)reader["pro_desc"],
                                pro_price = (int)reader["pro_price"],
                                pro_image = (string)reader["pro_image"]
                            };
                            pro.Add(p);
                        }
                    }
                }
            }
            ViewBag.cart = pro;
            return View();

        }

        public ActionResult Checkout()
        {
            HttpCookieCollection itemcookies = Request.Cookies;
            HttpCookie cuscookie = Request.Cookies["customer_id"];
            int customer = int.Parse(cuscookie.Value);
            string[] arr = itemcookies.AllKeys;

            Dictionary<string, string> kV = new Dictionary<string, string>();
            List<Product> pro = new List<Product>();
            foreach (var i in arr)
            {
                if (i.Length <= 3)
                {
                    for (int j = 0; j < int.Parse(Request.Cookies[i].Value); j++)
                    {
                        string guid = System.Guid.NewGuid().ToString();
                        using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
                        {
                            conn.Open();
                            string sql = @"INSERT INTO Purchaseitem (customer_id,pro_id,activation_code,purchase_time) " +
                                "values (" + customer + "," + i + ",'" + guid + "'," + DateTime.Now.ToString("MM/dd/yyyy") + ");";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Product p = new Product()
                                {
                                    pro_id = (int)reader["pro_id"],
                                    pro_name = (string)reader["pro_name"],
                                    pro_desc = (string)reader["pro_desc"],
                                    pro_price = (int)reader["pro_price"],
                                    pro_image = (string)reader["pro_image"]
                                };
                                pro.Add(p);
                            }
                        }
                    }
                }
            }
            return null;
        }


    }
}