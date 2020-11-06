using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string Country { get; set; }
        public string City { get; set; }

        public ICollection<int> BetsIds { get; set; }
        public ICollection<int> SaleLotsIds { get; set; }
        public ICollection<int> PurchasedLotsIds { get; set; }
    }
}
