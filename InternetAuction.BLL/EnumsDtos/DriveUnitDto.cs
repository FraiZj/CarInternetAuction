using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.EnumsDtos
{
    public enum DriveUnitDto
    {
        [Display(Name = "Not Specified")]
        NotSpecified,
        Other,
        [Display(Name = "Front-wheel drive")]
        FrontWheelDrive,
        [Display(Name = "Rear-whell drive")]
        RearWheelDrive,
        [Display(Name = "Four-wheel drive")]
        FourWheelDrive,
        [Display(Name = "Hybrid Synergy drive")]
        HybridSynergicDrive
    }
}
