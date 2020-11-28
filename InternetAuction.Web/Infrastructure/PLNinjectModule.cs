using AutoMapper;
using InternetAuction.BLL;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Services;
using Ninject.Modules;

namespace InternetAuction.Web.Infrastructure
{
    /// <summary>
    /// Represents PL DI module
    /// </summary>
    public class PlNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel
        /// </summary>
        public override void Load()
        {
            Bind<ILotService>().To<LotService>();
            Bind<IBetService>().To<BetService>();
            Bind<IUserService>().To<UserService>();
            Bind<ILoggerService>().To<LoggerService>();

            Bind<IMapper>().ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutomapperProfile()));
                return new Mapper(config);
            });
        }
    }
}