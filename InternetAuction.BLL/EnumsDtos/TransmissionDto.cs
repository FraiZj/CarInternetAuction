using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.EnumsDtos
{
    public enum TransmissionDto
    {
        [Display(Name = "Not Specified")]
        NotSpecified,
        Other,
        [Display(Name = "Automatic Transmission (AT)")]
        AT,
        [Display(Name = "Manual Transmission (MT)")]
        MT,
        [Display(Name = "Automated Manual Transmission (AM)")]
        AM,
        [Display(Name = "Continuously Variable Transmission (CVT)")]
        CVT
    }
}
