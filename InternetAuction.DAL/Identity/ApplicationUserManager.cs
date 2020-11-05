using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace InternetAuction.DAL.Identity
{
    /// <summary>
    /// UserManager for users where the primary key for the User is of type string
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Initializes an instance of the application user maneger with UserStore
        /// </summary>
        /// <param name="store"></param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        { }
    }
}
