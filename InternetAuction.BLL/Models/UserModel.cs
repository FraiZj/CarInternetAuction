using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string Country { get; set; }
        public string City { get; set; }

        [Display(Name = "Bets Ids")]
        public ICollection<int> BetsIds { get; set; }

        [Display(Name = "Sale Lots Ids")]
        public ICollection<int> SaleLotsIds { get; set; }

        [Display(Name = "Purchased Lots Ids")]
        public ICollection<int> PurchasedLotsIds { get; set; }
    }
}
