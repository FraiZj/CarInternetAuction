using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid First Name format")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid Last Name format")]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Bets")]
        public ICollection<int> BetsIds { get; set; }

        [Display(Name = "Sale Lots")]
        public ICollection<LotModel> SaleLots { get; set; }

        [Display(Name = "Purchased Lots")]
        public ICollection<LotModel> PurchasedLots { get; set; }

    }
}
