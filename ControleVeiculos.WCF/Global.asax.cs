using Autofac;
using System;
using Autofac.Integration.Wcf;
using ControleVeiculos.CrossCutting;

namespace ControleVeiculos.WCF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ConfigureDependencyInjection();
        }

        private void ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<SimuladorDetranSPService>();

            var container = DependencyRegister.Register(builder);
            AutofacHostFactory.Container = container;
        }
    }
}