using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public virtual ICollection<Lot> SoldLots { get; set; }
        public virtual ICollection<Lot> PurchasedLots { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
