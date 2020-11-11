using InternetAuction.DAL.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class Bet : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int LotId { get; set; }

        [Required]
        public DateTime BetDate { get; set; }

        [Required]
        public decimal Sum { get; set; }

        public virtual ApplicationUser User { get; set; }
        public Lot Lot { get; set; }
    }
}
