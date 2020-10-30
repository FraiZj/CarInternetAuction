namespace InternetAuction.DAL.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Mileage { get; set; }
        public string Transmission { get; set; }
        public string DriveUnit { get; set; }
        public string EngineType { get; set; }
        public string BodyType { get; set; }

        public TechnicalPassport TechnicalPassport { get; set; }
    }
}
