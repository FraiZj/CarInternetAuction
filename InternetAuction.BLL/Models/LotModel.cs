using InternetAuction.BLL.EnumsDtos;
using InternetAuction.BLL.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class LotModel : BaseModel
    {
        [Required, Display(Name = "Car")]
        public CarModel Car { get; set; }

        [Display(Name = "Seller Id")]
        public string SellerId { get; set; }

        [Display(Name = "Buyer Id")]
        public string BuyerId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Required, Display(Name = "Sale Type")]
        public SaleTypeDto SaleType { get; set; }

        [Display(Name = "Start Price($)")]
        public decimal StartPrice { get; set; } = 0;

        [Display(Name = "Turnkey Price($)")]
        public decimal? TurnkeyPrice { get; set; } = null;

        public ICollection<BetModel> Bets { get; set; }
    }
}
