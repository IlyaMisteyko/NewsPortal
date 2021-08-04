using NewsPortal.Logic.Common.Util;
using NewsPortal.Web.Util;
using Ninject;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NewsPortal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ServiceModule serviceModule = new ServiceModule();
            NinjectDependencyModule dependencyModule = new NinjectDependencyModule();
            AutoMapperModule automapperModule = new AutoMapperModule();
            var kernel = new StandardKernel(serviceModule, dependencyModule, automapperModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
