using AutoMapper;
using InternetAuction.BLL.EnumsDtos;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Enums;
using System.Linq;

namespace InternetAuction.BLL
{
    /// <summary>
    /// Provides a named configuration for maps.
    /// </summary>
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Initializes an instance of the automapper profile
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<ApplicationUser, UserModel>()
                .ForMember(u => u.BetsIds, opt => opt.MapFrom(appUser => appUser.Bets.Select(b => b.Id)))
                .ForMember(u => u.SaleLotsIds, opt => opt.MapFrom(appUser => appUser.SaleLots.Select(l => l.Id)))
                .ForMember(u => u.PurchasedLotsIds, opt => opt.MapFrom(appUser => appUser.PurchasedLots.Select(l => l.Id)))
                .ReverseMap();

            CreateMap<Lot, LotModel>()
                .ForMember(l => l.SellerId, opt => opt.MapFrom(lot => lot.Seller.Id))
                .ForMember(l => l.BuyerId, opt => opt.MapFrom(lot => lot.BuyerId))
                .ForMember(l => l.Bets, opt => opt.MapFrom(lot => lot.Bets.Select(b => b.Id)))
                .ReverseMap();

            CreateMap<Bet, BetModel>()
                .ForMember(b => b.UserId, opt => opt.MapFrom(bet => bet.User.Id))
                .ForMember(b => b.LotId, opt => opt.MapFrom(bet => bet.Lot))
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
