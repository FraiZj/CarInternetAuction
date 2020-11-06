using AutoMapper;
using InternetAuction.BLL.EnumsDtos;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Enums;
using System.Linq;

namespace InternetAuction.BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ApplicationUser, UserModel>()
                .ForMember(u => u.BetsIds, opt => opt.MapFrom(appUser => appUser.Bets.Select(b => b.Id)))
                .ForMember(u => u.SaleLotsIds, opt => opt.MapFrom(appUser => appUser.SaleLots.Select(l => l.Id)))
                .ForMember(u => u.PurchasedLotsIds, opt => opt.MapFrom(appUser => appUser.PurchasedLots.Select(l => l.Id)))
                .ReverseMap();

            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<TechnicalPassport, TechnicalPassportModel>().ReverseMap();
            CreateMap<CarImage, CarImageModel>().ReverseMap();

            CreateMap<Transmission, TransmissionDto>().ReverseMap();
            CreateMap<BodyType, BodyTypeDto>().ReverseMap();
            CreateMap<DriveUnit, DriveUnitDto>().ReverseMap();
            CreateMap<SaleType, SaleTypeDto>().ReverseMap();
        }
    }
}
