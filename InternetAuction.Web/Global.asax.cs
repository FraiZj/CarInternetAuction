using InternetAuction.BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InternetAuction.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule module = new InternetAuctionNinjectModule(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString);
            var kernel = new StandardKernel(module);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
