using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace InternetAuction.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        //public ApplicationDbContext()
        //    : this(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString)
        //{ } // ApplicationDbContext

        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<TechnicalPassport> TechnicalPassports { get; set; }
        public virtual DbSet<CarImage> CarImages { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<DriveUnit> DriveUnits { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }
        public virtual DbSet<BodyType> BodyTypes { get; set; }
        public virtual DbSet<SaleType> SaleTypes { get; set; }
    }

    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var driveUnits = new List<DriveUnit>
            {
                new DriveUnit { Id = 1, Name = "Front-wheel" },
                new DriveUnit { Id = 2, Name = "Rear drive" },
                new DriveUnit { Id = 3, Name = "Four-wheel drive" },
                new DriveUnit { Id = 4, Name = "Hybrid Synergic Drive" }
            };
            context.DriveUnits.AddRange(driveUnits);

            var bodyTypes = new List<BodyType>  
            {
                new BodyType { Id = 1, Name = "Hatch"},
                new BodyType { Id = 2, Name = "SUV"},
                new BodyType { Id = 3, Name = "Sedan"},
                new BodyType { Id = 4, Name = "Ute"},
                new BodyType { Id = 5, Name = "Cab Chassis"},
                new BodyType { Id = 6, Name = "Wagon"},
                new BodyType { Id = 7, Name = "Convetible"},
                new BodyType { Id = 8, Name = "Coupe"},
                new BodyType { Id = 9, Name = "People Mover"},
                new BodyType { Id = 10, Name = "Van"},
                new BodyType { Id = 11, Name = "Bus"},
                new BodyType { Id = 12, Name = "Light Truck"}
            };
            context.BodyTypes.AddRange(bodyTypes);

            var saleTypes = new List<SaleType>
            {
                new SaleType { Id = 1, Name = "Brand new"},
                new SaleType { Id = 2, Name = "Ex-demonstrator"},
                new SaleType { Id = 3, Name = "Press vehicles"},
                new SaleType { Id = 4, Name = "Run-out specials"},
                new SaleType { Id = 5, Name = "Second-hand, used or trade-in"},
                new SaleType { Id = 6, Name = "Private sales"},
                new SaleType { Id = 7, Name = "Imported "},
                new SaleType { Id = 8, Name = "Lease "}
            };
            context.SaleTypes.AddRange(saleTypes);

            var transmissions = new List<Transmission>
            {
                new Transmission { Id = 1, Name = "Automatic Transmission (AT)"},
                new Transmission { Id = 2, Name = "Manual Transmission (MT)"},
                new Transmission { Id = 3, Name = "Automated Manual Transmission (AM)"},
                new Transmission { Id = 4, Name = "Continuously Variable Transmission (CVT)"}
            };
            context.Transmissions.AddRange(transmissions);

            base.Seed(context);
        }
    }
}
