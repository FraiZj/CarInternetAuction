using InternetAuction.BLL.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class BetModel : BaseModel
    {
        [Required, Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required, Display(Name = "Lot Id")]
        public int LotId { get; set; }

        [Display(Name = "Bet Date")]
        public DateTime BetDate { get; set; }

        [Required, Display(Name = "Sum($)")]
        public decimal Sum { get; set; }
    }
}
