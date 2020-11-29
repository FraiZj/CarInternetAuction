using Microsoft.AspNet.Identity;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using System;

namespace InternetAuction.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = new TimeSpan(1, 0, 0)
            });
        }
    }
}