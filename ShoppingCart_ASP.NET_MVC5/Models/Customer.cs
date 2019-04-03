using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Models
{
    public class Customer
    {
        public int customer_id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string session_id { get; set; }
    }
}