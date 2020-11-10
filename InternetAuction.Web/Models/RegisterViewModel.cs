using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.Models
{
    public class RegisterViewModel
    {
        [Required, RegularExpression(@"^[a-zA-Z_-]+$"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z_-]+$"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}