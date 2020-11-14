using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.EnumsDtos
{
    public enum BodyTypeDto
    {
        None,
        Other,
        Hatch,
        SUV,
        Sedan,
        Ute,
        [Display(Name = "Cab Chassis")]
        CabChassis,
        Wagon,
        Convetible,
        Coupe,
        [Display(Name = "People Mover")]
        PeopleMover,
        Van,
        Bus,
        [Display(Name = "Light Truck")]
        LightTruck,
    }
}
