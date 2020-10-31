using InternetAuction.DAL.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class Car : BaseEntity
    {

        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string Brand { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9_-]*$")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public int TransmissionId { get; set; }

        [Required]
        public int DriveUnitId { get; set; }

        [Required]
        public string EngineType { get; set; }

        [Required]
        public int BodyTypeId { get; set; }

        public TechnicalPassport TechnicalPassport { get; set; }
        public Transmission Transmission { get; set; }
        public DriveUnit DriveUnit { get; set; }
        public BodyType BodyType { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
    }
}
