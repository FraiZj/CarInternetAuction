using System;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class ExceptionLogModel
    {
        public int Id { get; set; }

        [Display(Name = "Exception Message")]
        public string ExceptionMessage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }

        [Display(Name = "Date and time")]
        public DateTime DateTime { get; set; }
    }
}
