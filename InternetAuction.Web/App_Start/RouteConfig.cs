using System.Web.Mvc;
using System.Web.Routing;

namespace InternetAuction.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AccountDefaultRoute",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "UserProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Lots", action = "ActiveLots", id = UrlParameter.Optional }
            );
        }
    }
}
