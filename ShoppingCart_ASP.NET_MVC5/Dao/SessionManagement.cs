using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class SessionManagement
    { 
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
        public static void RemoveSession(string sessionId)
        {
            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpCookie c = HttpContext.Current.Request.Cookies[key];
                c.Expires = DateTime.Now.AddMonths(-1);
                HttpContext.Current.Response.AppendCookie(c);
            } 
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