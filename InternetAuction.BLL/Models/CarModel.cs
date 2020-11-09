using InternetAuction.BLL.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class CarModel : BaseModel
    {
        [Required, RegularExpression(@"^[a-zA-Z_-]+$")]
        public string Brand { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9_-]*$")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required, Display(Name = "Engine Type")]
        public string EngineType { get; set; }

        [Required, Display(Name = "Technical Passport")]
        public TechnicalPassportModel TechnicalPassport { get; set; }

        [Display(Name = "Car Images")]
        public ICollection<CarImageModel> CarImages { get; set; }
    }
}
