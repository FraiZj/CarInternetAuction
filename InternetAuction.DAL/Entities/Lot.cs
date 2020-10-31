using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.DAL.Entities
{
    public class Lot
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int SellerId { get; set; }
        public int BuyerId { get; set; }

        [Required]
        public DateTime AuctionDate { get; set; }

        [Required]
        public int SaleTypeId { get; set; }

        public Car Car { get; set; }
        public ApplicationUser Seller { get; set; }
        public ApplicationUser Buyer { get; set; }
        public SaleType SaleType { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
