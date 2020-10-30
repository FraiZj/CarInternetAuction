using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetAuction.DAL.Entities
{
    public class TechnicalPassport
    {
        [Key, ForeignKey("Car")]
        public int CarId { get; set; }
        public string VIN { get; set; }
        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
        public string Features { get; set; }
        public bool IsMileageConfirmed { get; set; }
        public bool HasKeys { get; set; }
        public string Location { get; set; }

        public Car Car { get; set; }
    }
}
