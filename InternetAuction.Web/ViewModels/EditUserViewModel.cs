using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Zа-яА-я-_ ]+$", ErrorMessage = "Invalid first name format")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Zа-яА-я-_ ]+$", ErrorMessage = "Invalid last name format")]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}