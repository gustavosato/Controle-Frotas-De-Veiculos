using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Lean.Test.CloudWebApi.App_Start;
using Autofac.Integration.WebApi;
using Lean.Test.Cloud.CrossCutting;
using System.Reflection;
using Autofac;
using Swashbuckle.Application;
using System;
using FluentValidation.WebApi;

[assembly: OwinStartup(typeof(Lean.Test.CloudWebApi.Startup))]

namespace Lean.Test.CloudWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureDependencyInjection(config);

            WebApiConfig.Register(config);
            FluentValidationModelValidatorProvider.Configure(config);

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "LeanTest");
                c.IncludeXmlComments(GetXmlCommentsPath());
            }).EnableSwaggerUi();

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureDependencyInjection(HttpConfiguration config)
        {
            var container = new ContainerBuilder();

            container.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var builder = DependencyRegister.Register(container);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder);
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\Lean.Test.Cloud.WebApi.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
