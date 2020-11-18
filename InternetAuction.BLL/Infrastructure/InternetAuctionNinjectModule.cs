using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Services;
using InternetAuction.DAL;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using Ninject.Modules;

namespace InternetAuction.BLL.Infrastructure
{
    /// <summary>
    /// Represents DI module
    /// </summary>
    public class InternetAuctionNinjectModule : NinjectModule
    {
        private readonly string nameOrConnectionStringForContext;

        /// <summary>
        /// Initializes module with connection string
        /// </summary>
        /// <param name="nameOrConnectionStringForContext"></param>
        public InternetAuctionNinjectModule(string nameOrConnectionStringForContext)
        {
            this.nameOrConnectionStringForContext = nameOrConnectionStringForContext;
        }

        public override void Load()
        {
            Bind<ApplicationDbContext>().ToSelf()
                .WithConstructorArgument("nameOrConnectionString", nameOrConnectionStringForContext);
            Bind<ICarRepository>().To<CarRepository>();
            Bind<ILotRepository>().To<LotRepository>();
            Bind<IBetRepository>().To<BetRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<ILotService>().To<LotService>();
            Bind<IBetService>().To<BetService>();
            Bind<IUserService>().To<UserService>();

            Bind<IMapper>().ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutomapperProfile()));
                return new Mapper(config);
            });
        }
    }
}
