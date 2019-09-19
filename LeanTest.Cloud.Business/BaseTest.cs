using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Lean.Test.Cloud.CrossCutting;

namespace Lean.Test.CloudTests
{
    public class BaseTest
    {
        private ContainerBuilder builder = new ContainerBuilder();
        protected static IContainer container;
        protected static AutofacServiceLocator serviceLocator;

        public BaseTest()
        {
            container = DependencyRegister.Register(builder);
            serviceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }
    }
}
