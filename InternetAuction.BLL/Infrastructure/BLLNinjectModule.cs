using InternetAuction.DAL;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using Ninject.Modules;

namespace InternetAuction.BLL.Infrastructure
{
    /// <summary>
    /// Represents BLL DI module
    /// </summary>
    public class BllNinjectModule : NinjectModule
    {
        private readonly string nameOrConnectionStringForContext;

        /// <summary>
        /// Initializes module with connection string
        /// </summary>
        /// <param name="nameOrConnectionStringForContext"></param>
        public BllNinjectModule(string nameOrConnectionStringForContext)
        {
            this.nameOrConnectionStringForContext = nameOrConnectionStringForContext;
        }

        /// <summary>
        /// Loads the module into the kernel
        /// </summary>
        public override void Load()
        {
            Bind<ApplicationDbContext>().ToSelf()
                .WithConstructorArgument("nameOrConnectionString", nameOrConnectionStringForContext);
            Bind<ICarRepository>().To<CarRepository>();
            Bind<ILotRepository>().To<LotRepository>();
            Bind<IBetRepository>().To<BetRepository>();
            Bind<ILogger>().To<Logger>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
