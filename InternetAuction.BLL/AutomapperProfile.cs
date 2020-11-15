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
                .ReverseMap();

            CreateMap<Lot, LotModel>()
                .ReverseMap();

            CreateMap<Bet, BetModel>()
                .ReverseMap();

            CreateMap<Car, CarModel>()
                .ForMember(c => c.CarImages, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TechnicalPassport, TechnicalPassportModel>().ReverseMap();

            CreateMap<Transmission, TransmissionDto>().ReverseMap();
            CreateMap<BodyType, BodyTypeDto>().ReverseMap();
            CreateMap<DriveUnit, DriveUnitDto>().ReverseMap();
            CreateMap<SaleType, SaleTypeDto>().ReverseMap();
        }
    }
}
