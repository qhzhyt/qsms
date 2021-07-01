using System.Web.Mvc;
using System.Web.Routing;

namespace qssWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //routes.Ignore("*.cshtml");
            routes.IgnoreRoute("Content/{*relpath}");
            routes.IgnoreRoute("Scripts/{*relpath}");
            routes.IgnoreRoute("fonts/{*relpath}");
            routes.IgnoreRoute("images/{*relpath}");
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}