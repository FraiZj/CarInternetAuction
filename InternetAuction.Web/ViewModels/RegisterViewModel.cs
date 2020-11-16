﻿using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, EmailAddress]
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
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "User Name"), RegularExpression(@"^[a-zA-Z0-9-_]+$")]
        public string UserName { get; set; }
    }
}