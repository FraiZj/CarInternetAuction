using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternetAuction.Web.Startup))]
namespace InternetAuction.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
