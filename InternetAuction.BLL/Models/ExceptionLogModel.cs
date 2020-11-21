using System;

namespace InternetAuction.BLL.Models
{
    public class ExceptionLogModel
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }
        public DateTime DateTime { get; set; }
    }
}
