using System;

namespace InternetAuction.DAL.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LotId { get; set; }
        public DateTime BetDate { get; set; }
        public decimal Sum { get; set; }

        public ApplicationUser User { get; set; }
        public Lot Lot { get; set; }
    }
}
