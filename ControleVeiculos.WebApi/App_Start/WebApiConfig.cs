using ControleVeiculos.WebApi.Infrastrucure.Formatter;
using System.Web.Http;

namespace ControleVeiculosWebApi.App_Start
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