using Lean.Test.Cloud.WebApi.Infrastrucure.Formatter;
using System.Web.Http;

namespace Lean.Test.CloudWebApi.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new TextMediaTypeFormatter());
        }
    }
}