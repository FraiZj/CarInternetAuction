using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace InternetAuction.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public ApplicationDbContext()
        //{

        //}

        public ApplicationDbContext()
            : this(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString)
        { } // ApplicationDbContext

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
    }

    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //var driveUnits = new List<DriveUnit>
            //{
            //    new DriveUnit { Name = "Front-wheel" },
            //    new DriveUnit { Name = "Rear drive" },
            //    new DriveUnit { Name = "Four-wheel drive" },
            //    new DriveUnit { Name = "Hybrid Synergic Drive" }
            //};
            //context.DriveUnits.AddRange(driveUnits);

            //var bodyTypes = new List<BodyType>  
            //{
            //    new BodyType { Name = "Hatch"},
            //    new BodyType { Name = "SUV"},
            //    new BodyType { Name = "Sedan"},
            //    new BodyType { Name = "Ute"},
            //    new BodyType { Name = "Cab Chassis"},
            //    new BodyType { Name = "Wagon"},
            //    new BodyType { Name = "Convetible"},
            //    new BodyType { Name = "Coupe"},
            //    new BodyType { Name = "People Mover"},
            //    new BodyType { Name = "Van"},
            //    new BodyType { Name = "Bus"},
            //    new BodyType { Name = "Light Truck"}
            //};
            //context.BodyTypes.AddRange(bodyTypes);

            //var saleTypes = new List<SaleType>
            //{
            //    new SaleType { Name = "Brand new"},
            //    new SaleType { Name = "Ex-demonstrator"},
            //    new SaleType { Name = "Press vehicles"},
            //    new SaleType { Name = "Run-out specials"},
            //    new SaleType { Name = "Second-hand, used or trade-in"},
            //    new SaleType { Name = "Private sales"},
            //    new SaleType { Name = "Imported "},
            //    new SaleType { Name = "Lease "}
            //};
            //context.SaleTypes.AddRange(saleTypes);

            //var transmissions = new List<Transmission>
            //{
            //    new Transmission { Name = "Automatic Transmission (AT)"},
            //    new Transmission { Name = "Manual Transmission (MT)"},
            //    new Transmission { Name = "Automated Manual Transmission (AM)"},
            //    new Transmission { Name = "Continuously Variable Transmission (CVT)"}
            //};
            //context.Transmissions.AddRange(transmissions);

            base.Seed(context);
        }
    }
}
