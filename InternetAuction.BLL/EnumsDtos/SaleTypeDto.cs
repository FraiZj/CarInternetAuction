using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.EnumsDtos
{
    public enum SaleTypeDto
    {
        None,
        Other,
        [Display(Name = "Brand new")]
        BrandNew,
        [Display(Name = "Ex-demonstrator")]
        ExDemonstrator,
        [Display(Name = "Press vehicle")]
        PressVehicle,
        [Display(Name = "Run-out special")]
        RunOutSpecial,
        [Display(Name = "Second-hand, used or trade-in")]
        SecondHandOrTradeIn,
        [Display(Name = "Private sales")]
        PrivateSales,
        Imported,
        Lease,
    }
}
