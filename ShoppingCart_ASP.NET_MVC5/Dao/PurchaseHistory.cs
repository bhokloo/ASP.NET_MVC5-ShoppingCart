using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Dao
{
    public class PurchaseHistory
    {
        public static List<AllItemList> allhistory(string customer_id)
        {
            Debug.WriteLine(customer_id);
            List<AllItemList> Listall = new List<AllItemList>();
            Debug.WriteLine(customer_id);
            using (SqlConnection conn = new SqlConnection(("Server=.; Database=ShoppingCartT4; Integrated Security=true")))
            {
                Debug.WriteLine("came her");
                conn.Open();
                string sql = @"select * from Purchaseitem p join Customer c on c.customer_id = p.customer_id join Product po on po.pro_id = p.pro_id where c.customer_id = '" + customer_id + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Debug.WriteLine("Looping");
                    AllItemList all = new AllItemList();

                    all.pro_id = (int)reader["pro_id"];
                    all.pro_name = (string)reader["pro_name"];
                    all.pro_desc = (string)reader["pro_desc"];
                    all.pro_price = (int)reader["pro_price"];
                    all.pro_image = (string)reader["pro_image"];
                    all.activation_code = (string)reader["activation_code"];
                    all.purchase_time = (DateTime)reader["purchase_time"];
                   
                 Listall.Add(all);
                 Debug.WriteLine(all.pro_id);
                }
               
            }
            return Listall;
        }
    }
}
