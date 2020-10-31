using InternetAuction.DAL.Entities.Base;
using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class SaleType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Lot> Lots { get; set; }
    }
}
