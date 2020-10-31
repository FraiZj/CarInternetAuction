using InternetAuction.DAL.Entities.Base;

namespace InternetAuction.DAL.Entities
{
    public class CarImage : BaseEntity
    {
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}