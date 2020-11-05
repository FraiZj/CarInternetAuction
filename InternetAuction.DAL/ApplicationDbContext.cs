using InternetAuction.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
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
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lot>()
                    .HasOptional(l => l.Seller)
                    .WithMany(u => u.SaleLots)
                    .HasForeignKey(l => l.SellerId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                        .HasOptional(l => l.Buyer)
                        .WithMany(u => u.PurchasedLots)
                        .HasForeignKey(l => l.BuyerId)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<TechnicalPassport> TechnicalPassports { get; set; }
        public virtual DbSet<CarImage> CarImages { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Bet> Bets { get; set; }
    }
}
