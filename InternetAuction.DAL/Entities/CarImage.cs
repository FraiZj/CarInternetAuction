namespace InternetAuction.DAL.Entities
{
    public class CarImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}