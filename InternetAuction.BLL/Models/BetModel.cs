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

        [Required, Display(Name = "Bet Date")]
        public DateTime BetDate { get; set; }

        [Required]
        public decimal Sum { get; set; }
    }
}
