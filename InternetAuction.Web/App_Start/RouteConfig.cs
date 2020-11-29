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
                name: "Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "UserProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserProfileEdit",
                url: "Account/Edit/{id}",
                defaults: new { controller = "Account", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Details",
                url: "Lots/Details/{id}",
                defaults: new { controller = "Lots", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LotEdit",
                url: "Lots/Edit/{id}",
                defaults: new { controller = "Lots", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Lots", action = "ActiveLots" }
            );
        }
    }
}
