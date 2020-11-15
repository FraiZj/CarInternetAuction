using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class BetViewModel
    {
        [Required]
        public int LotId { get; set; }

        [Required, Display(Name = "Sum($)")]
        public decimal Sum { get; set; }
    }
}