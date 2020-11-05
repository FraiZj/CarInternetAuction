using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetAuction.DAL.Identity
{
    /// <summary>
    /// Exposes role related api which will automatically save changes to the RoleStore
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        /// <summary>
        /// Initializes an instance of the application role maneger with RoleStore
        /// </summary>
        /// <param name="store">RoleStore</param>
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
            : base(store)
        { }
    }
}
