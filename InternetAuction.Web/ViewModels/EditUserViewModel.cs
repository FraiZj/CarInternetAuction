﻿using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}