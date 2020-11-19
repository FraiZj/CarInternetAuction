using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class UserSearchModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
