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

        [Display(Name = "Start Price (Max. 1 000 000 $)")]
        [Required]
        public decimal StartPrice { get; set; }

        [Display(Name = "Turnkey Price (Max. 1 000 000 $)")]
        [Required]
        public decimal TurnkeyPrice { get; set; }

        public ICollection<BetModel> Bets { get; set; }
    }
}
