using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ComparePassword = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace InternetAuction.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid first name format")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid last name format")]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "Email already in use")]
        public string Email { get; set; }

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [ComparePassword("Password")]
        public string ConfirmPassword { get; set; }
    }
}