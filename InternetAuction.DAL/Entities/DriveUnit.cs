using System.Collections.Generic;

namespace InternetAuction.DAL.Entities
{
    public class DriveUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
