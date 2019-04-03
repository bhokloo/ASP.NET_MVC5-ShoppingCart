using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class SessionManagement { 
        //Creating Session
        public static string CreateSession(int customer_id)
        {
            string sessionId = Guid.NewGuid().ToString();
            Debug.WriteLine("Customer id" + customer_id);
            Debug.WriteLine("Session id" + sessionId);
            using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE Customer SET session_id = '" + sessionId + "'" + " WHERE customer_id =" + customer_id;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            return sessionId;
        }


        //Destroying Session
        public static void RemoveSession(string sessionId, string username)
        {
            HttpCookie cookie = new HttpCookie("customer_id");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);

            using (SqlConnection conn = new SqlConnection("Server=.; Database=ShoppingCartT4; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE Customer SET session_id = NULL WHERE session_id = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

        }
    }
}