using InternetAuction.BLL.EnumsDtos;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class TechnicalPassportModel
    {
        public int CarId { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9]+$")]
        [MinLength(17), MaxLength(17)]
        public string VIN { get; set; }

        [Required]
        public TransmissionDto Transmission { get; set; }

        [Required, Display(Name = "Drive Unit")]
        public DriveUnitDto DriveUnit { get; set; }

        [Required, Display(Name = "Body Type")]
        public BodyTypeDto BodyType { get; set; }

        [Display(Name = "Primary Damage")]
        public string PrimaryDamage { get; set; }

        [Required, Display(Name = "Has Keys")]
        public bool HasKeys { get; set; }
    }
}
