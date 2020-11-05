using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetAuction.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager()
            : base(new UserStore<ApplicationUser>())
        { }

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        { }
    }
}
