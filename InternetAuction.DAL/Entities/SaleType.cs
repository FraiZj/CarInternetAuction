using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class SaleType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Lot> Lots { get; set; }
    }
}
