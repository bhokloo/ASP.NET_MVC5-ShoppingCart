using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart_ASP.NET_MVC5.Models
{
    public class Customer
    {
        public int customer_id { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        public string password { get; set; }

        public string firstname { get; set; }

        public string session_id { get; set; }
    }
}