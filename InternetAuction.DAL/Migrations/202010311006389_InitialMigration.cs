namespace InternetAuction.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LotId = c.Int(nullable: false),
                        BetDate = c.DateTime(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.LotId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                        BuyerId = c.Int(nullable: false),
                        AuctionDate = c.DateTime(nullable: false),
                        SaleTypeId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        Buyer_Id = c.String(maxLength: 128),
                        Seller_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.Buyer_Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.SaleTypes", t => t.SaleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id)
                .Index(t => t.CarId)
                .Index(t => t.SaleTypeId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.Buyer_Id)
                .Index(t => t.Seller_Id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Country = c.String(nullable: false),
                    City = c.String(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        TransmissionId = c.Int(nullable: false),
                        DriveUnitId = c.Int(nullable: false),
                        EngineType = c.String(nullable: false),
                        BodyTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BodyTypes", t => t.BodyTypeId, cascadeDelete: true)
                .ForeignKey("dbo.DriveUnits", t => t.DriveUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Transmissions", t => t.TransmissionId, cascadeDelete: true)
                .Index(t => t.TransmissionId)
                .Index(t => t.DriveUnitId)
                .Index(t => t.BodyTypeId);
            
            CreateTable(
                "dbo.BodyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriveUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechnicalPassports",
                c => new
                    {
                        CarId = c.Int(nullable: false),
                        VIN = c.String(nullable: false, maxLength: 17),
                        PrimaryDamage = c.String(),
                        SecondaryDamage = c.String(),
                        Features = c.String(),
                        IsMileageConfirmed = c.Boolean(nullable: false),
                        HasKeys = c.Boolean(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lots", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "SaleTypeId", "dbo.SaleTypes");
            DropForeignKey("dbo.Lots", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.TechnicalPassports", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "DriveUnitId", "dbo.DriveUnits");
            DropForeignKey("dbo.Cars", "BodyTypeId", "dbo.BodyTypes");
            DropForeignKey("dbo.Lots", "Buyer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bets", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bets", "LotId", "dbo.Lots");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TechnicalPassports", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "BodyTypeId" });
            DropIndex("dbo.Cars", new[] { "DriveUnitId" });
            DropIndex("dbo.Cars", new[] { "TransmissionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Lots", new[] { "Seller_Id" });
            DropIndex("dbo.Lots", new[] { "Buyer_Id" });
            DropIndex("dbo.Lots", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Lots", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Lots", new[] { "SaleTypeId" });
            DropIndex("dbo.Lots", new[] { "CarId" });
            DropIndex("dbo.Bets", new[] { "User_Id" });
            DropIndex("dbo.Bets", new[] { "LotId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SaleTypes");
            DropTable("dbo.Transmissions");
            DropTable("dbo.TechnicalPassports");
            DropTable("dbo.DriveUnits");
            DropTable("dbo.BodyTypes");
            DropTable("dbo.Cars");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Lots");
            DropTable("dbo.Bets");
        }
    }
}
