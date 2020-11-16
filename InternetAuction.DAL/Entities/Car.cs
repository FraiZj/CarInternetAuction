using InternetAuction.DAL.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class Car : BaseEntity
    {

        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$")]
        public string Brand { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9-_ ]+$")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string EngineType { get; set; }

        public virtual TechnicalPassport TechnicalPassport { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
    }
}
