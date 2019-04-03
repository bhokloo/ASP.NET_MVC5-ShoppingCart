using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult Partial(string search)

        {
            List<Product> pro = new List<Product>();
            using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"select * from Product where pro_desc like '%"+ search +"%'";
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
            ViewData["partial"] = pro;
            ViewBag.a = 2;
            return View("../Home/Home");
        }
    }
}