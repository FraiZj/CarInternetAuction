using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string LastName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string Country { get; set; }
        public string City { get; set; }


        public virtual ICollection<Lot> SaleLots { get; set; } 
        public virtual ICollection<Lot> PurchasedLots { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
