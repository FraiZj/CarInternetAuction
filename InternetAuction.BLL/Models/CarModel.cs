using InternetAuction.BLL.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InternetAuction.BLL.Models
{
    public class CarModel : BaseModel
    {
        [Required, RegularExpression(@"^[a-zA-Zа-яА-я-_ ]+$", ErrorMessage = "Invalid car brand format")]
        [MaxLength(30)]
        public string Brand { get; set; }

        [Required, RegularExpression(@"^[a-zA-Zа-яА-я0-9-_ ]+$", ErrorMessage = "Invalid car model format")]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required, Display(Name = "Engine Type")]
        [MaxLength(30)]
        public string EngineType { get; set; }

        [Required, Display(Name = "Technical Passport")]
        public TechnicalPassportModel TechnicalPassport { get; set; }

        [Display(Name = "Car Images")]
        public ICollection<ImageModel> CarImages { get; set; }

        [Display(Name = "Files")]
        public ICollection<HttpPostedFileBase> Files { get; set; }
    }
}
