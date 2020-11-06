using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class TechnicalPassportModel
    {
        public int CarId { get; set; }

        [Required, StringLength(17), RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string VIN { get; set; }

        [Display(Name = "Primary Damage")]
        public string PrimaryDamage { get; set; }

        [Display(Name = "Secondary Damage")]
        public string SecondaryDamage { get; set; }

        public string Features { get; set; }

        [Required, Display(Name = "Is Mileage Confirmed")]
        public bool IsMileageConfirmed { get; set; }

        [Required, Display(Name = "Has Keys")]
        public bool HasKeys { get; set; }
        public string Location { get; set; }
    }
}
