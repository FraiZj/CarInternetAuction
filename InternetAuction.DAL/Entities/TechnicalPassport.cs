using InternetAuction.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetAuction.DAL.Entities
{
    public class TechnicalPassport
    {
        [Key, ForeignKey("Car")]
        public int CarId { get; set; }

        [Required, StringLength(17), RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string VIN { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        public DriveUnit DriveUnit { get; set; }

        [Required]
        public BodyType BodyType { get; set; }

        public string PrimaryDamage { get; set; }

        [Required]
        public bool HasKeys { get; set; }

        public Car Car { get; set; }
    }
}
