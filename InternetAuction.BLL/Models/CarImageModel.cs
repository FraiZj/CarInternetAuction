using InternetAuction.BLL.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class CarImageModel : BaseModel
    {
        public string Title { get; set; }
        public byte[] Data { get; set; }

        [Display(Name = "Car Id")]
        public int CarId { get; set; }
    }
}
