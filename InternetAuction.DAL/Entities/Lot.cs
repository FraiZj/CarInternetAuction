using InternetAuction.DAL.Entities.Base;
using InternetAuction.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetAuction.DAL.Entities
{
    public class Lot : BaseEntity
    {
        [Required]
        public int CarId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }

        [Required]
        public SaleType SaleType { get; set; }

        [Required]
        public DateTime AuctionDate { get; set; }

        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; }
        [ForeignKey("BuyerId")]
        public ApplicationUser Buyer { get; set; }
        public Car Car { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
