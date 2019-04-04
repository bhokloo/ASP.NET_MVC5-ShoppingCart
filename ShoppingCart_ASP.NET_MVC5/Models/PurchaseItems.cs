using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Models
{
    public class PurchaseItems
    {
        public int customer_id { get; set; }
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public string pro_desc { get; set; }
        public string pro_image { get; set; }
        public List<string> activation_code { get; set; }
        public DateTime purchase_time { get; set; }
        public int count { get; set; }
    }
}