using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
