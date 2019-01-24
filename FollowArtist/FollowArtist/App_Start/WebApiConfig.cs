using System.Web.Http;

namespace FollowArtist
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            
            //////////////////////////////////////////////////////
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "ApiById",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByName",
                routeTemplate: "api/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { id = @"^[a-z]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/{controller}/{Action}",
                defaults: new { action = "Get" }
            );
            ////////////////////////////////////////////////////////
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
