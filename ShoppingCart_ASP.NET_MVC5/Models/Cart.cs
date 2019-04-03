using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Models
{
    public class Cart
    {
        public int pro_id { get; set; }

        public string pro_name { get; set; }

        public string pro_desc { get; set; }

        public int pro_price { get; set; }

        public string pro_image { get; set; }

        public int count { get; set; } 
    }
}