using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class ProductDetails
    {
        public static List<Product> callme()
        {
            List<Product> pro = new List<Product>();
            using (SqlConnection conn = new SqlConnection(("Server=.; Database=ShoppingCartT4; Integrated Security=true")))
            {
                conn.Open();
                string sql = @"SELECT * from Product";
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
                    Debug.WriteLine(p.pro_id);
                }
            }
            return pro;
        }
    }
}