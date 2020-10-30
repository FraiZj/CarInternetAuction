using System;
using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class Lot
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public DateTime AuctionDate { get; set; }
        public int SaleType { get; set; }

        public Car Car { get; set; }
        public ApplicationUser Seller { get; set; }
        public ApplicationUser Buyer { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
