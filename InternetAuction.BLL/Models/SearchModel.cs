using InternetAuction.BLL.EnumsDtos;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class SearchModel
    {
        public string Brand { get; set; }

        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Display(Name = "Drive Unit")]
        public DriveUnitDto DriveUnit { get; set; }

        [Display(Name = "Body Type")]
        public BodyTypeDto BodyType { get; set; }

        [Display(Name = "Min Price")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public decimal MaxPrice { get; set; } = 1000000;
    }
}
