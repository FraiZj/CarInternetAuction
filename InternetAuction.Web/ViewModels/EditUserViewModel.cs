﻿using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid first name format")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$", ErrorMessage = "Invalid last name format")]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}