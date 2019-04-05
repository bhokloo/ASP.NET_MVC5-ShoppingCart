using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class CustomerDetails
    {
        public static Customer GetCustomer(string username)

        {
            Customer customer = null;
            List<Customer> customerlist = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(("Server=.; Database=ShoppingCartT4; Integrated Security=true")))
            {
                conn.Open();
                string sql = @"SELECT customer_id, username, firstname, password from Customer WHERE username = '" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        customer_id = (int)reader["customer_id"],
                        username = (string)reader["username"],
                        password = (string)reader["password"],
                        firstname = (string)reader["firstname"]
                    };
                }
            }
            return customer;
        }
    }
}