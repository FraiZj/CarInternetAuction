using System.ComponentModel.DataAnnotations;

namespace InternetAuction.Web.ViewModels
{
    public class BetViewModel
    {
        public int LotId { get; set; }

        [Required]
        public decimal Sum { get; set; }
    }
}