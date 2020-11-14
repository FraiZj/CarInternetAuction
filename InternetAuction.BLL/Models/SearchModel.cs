using InternetAuction.BLL.EnumsDtos;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class SearchModel
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        [Display(Name = "Sale Type")]
        public SaleTypeDto? SaleType { get; set; }

        public TransmissionDto? Transmission { get; set; }

        [Display(Name = "Drive Unit")]
        public DriveUnitDto? DriveUnit { get; set; }

        [Display(Name = "Body Type")]
        public BodyTypeDto? BodyType { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}
