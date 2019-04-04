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
            List<Cart> pro = new List<Cart>();
            foreach (var i in arr)
            {
                if (i.Any(Char.IsDigit))
                {
                    using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
                    {
                        conn.Open();
                        string sql = @"select * from Product where pro_id = '" + i + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Cart p = new Cart()
                            {
                                pro_id = (int)reader["pro_id"],
                                pro_name = (string)reader["pro_name"],
                                pro_desc = (string)reader["pro_desc"],
                                pro_price = (int)reader["pro_price"],
                                pro_image = (string)reader["pro_image"],
                                count = int.Parse(Request.Cookies[i].Value)
                            };
                            pro.Add(p);
                        }
                    }
                }
            }
            ViewBag.Count = int.Parse(Request.Cookies["Count"].Value);
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
                                "values (" + customer + "," + i + ",'" + guid + "','" + DateTime.Now.ToString("yyyy-mm-dd") + "');";
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
            return RedirectToAction("MyPurchases");
        }

        public ActionResult MyPurchases()
        {
            string customer_id = Request.Cookies["customer_id"].Value;
            List<PurchaseItems> allitems = new List<PurchaseItems>();
            using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true; MultipleActiveResultSets=True"))
            {
                conn.Open();
                string sql = @"select purchase_time, i.pro_id, pro_name, pro_desc, pro_image, count(i.pro_id) as count from Purchaseitem i join Product p on i.pro_id = p.pro_id where customer_id ='" + customer_id + "' group by i.pro_id, purchase_time, pro_name, pro_desc, pro_image";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PurchaseItems singleitem = new PurchaseItems();
                    singleitem.purchase_time = (DateTime)reader["purchase_time"];
                    singleitem.pro_id = (int)reader["pro_id"];
                    singleitem.pro_desc = (string)reader["pro_desc"];
                    singleitem.pro_image = (string)reader["pro_image"];
                    singleitem.pro_name = (string)reader["pro_name"];
                    singleitem.count = (int)reader["count"];
                    List<string> arr = new List<string>();

                    using (SqlConnection conn1 = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true; MultipleActiveResultSets=True"))
                    {
                        conn1.Open();

                        string sql1 = @"select activation_code from Purchaseitem where purchase_time='" + singleitem.purchase_time.ToString("MM/dd/yyyy") + "' and pro_id = '" + singleitem.pro_id + "'";
                        SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        SqlDataReader reader1 = cmd1.ExecuteReader();

                        while (reader1.Read())
                        {
                            arr.Add((string)reader1["activation_code"]);
                        }

                    }
                    singleitem.activation_code = arr;
                    allitems.Add(singleitem);
                }

            }
            ViewBag.purchases = allitems;
            return View();
        }


    }
}