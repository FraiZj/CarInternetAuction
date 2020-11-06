using InternetAuction.BLL.EnumsDtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class CarModel
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
        public TransmissionDto Transmission { get; set; }

        [Required, Display(Name = "Drive Unit")]
        public DriveUnitDto DriveUnit { get; set; }

        [Required, Display(Name = "Engine Type")]
        public string EngineType { get; set; }

        [Required, Display(Name = "Body Type")]
        public BodyTypeDto BodyType { get; set; }

        public virtual TechnicalPassportModel TechnicalPassport { get; set; }
        public virtual ICollection<CarImageModel> CarImages { get; set; }
    }
}
