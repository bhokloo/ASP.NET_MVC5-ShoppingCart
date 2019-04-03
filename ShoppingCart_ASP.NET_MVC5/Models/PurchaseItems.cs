using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Models
{
    public class PurchaseItems
    {
        public int purchase_id { get; set; }

        public int customer_id { get; set; }

        public int pro_id { get; set; }

        public string activation_code { get; set; }

        public DateTime purchase_time { get; set; }
    }
}