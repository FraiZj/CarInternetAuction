using InternetAuction.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetAuction.DAL.Entities
{
    public class TechnicalPassport
    {
        [Key, ForeignKey("Car")]
        public int CarId { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9]+$")]
        [MinLength(17), MaxLength(17)]
        public string VIN { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        public DriveUnit DriveUnit { get; set; }

        [Required]
        public BodyType BodyType { get; set; }

        [MaxLength(50)]
        public string PrimaryDamage { get; set; }

        [Required]
        public bool HasKeys { get; set; }

        public Car Car { get; set; }
    }
}
