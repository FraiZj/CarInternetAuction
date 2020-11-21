using System;

namespace InternetAuction.DAL.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }
        public DateTime DateTime { get; set; }
    }
}
