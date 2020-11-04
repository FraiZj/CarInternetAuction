using InternetAuction.DAL.Entities.Base;
using InternetAuction.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class Lot : BaseEntity
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public string SellerId { get; set; }
        public string BuyerId { get; set; }

        [Required]
        public DateTime AuctionDate { get; set; }

        [Required]
        public SaleType SaleType { get; set; }

        public Car Car { get; set; }
        public ApplicationUser Seller { get; set; }
        public ApplicationUser Buyer { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
