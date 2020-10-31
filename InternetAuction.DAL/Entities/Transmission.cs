using InternetAuction.DAL.Entities.Base;
using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class Transmission : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
