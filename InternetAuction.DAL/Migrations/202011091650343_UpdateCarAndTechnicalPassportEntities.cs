namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCarAndTechnicalPassportEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalPassports", "Mileage", c => c.Int(nullable: false));
            AddColumn("dbo.TechnicalPassports", "Transmission", c => c.Int(nullable: false));
            AddColumn("dbo.TechnicalPassports", "DriveUnit", c => c.Int(nullable: false));
            AddColumn("dbo.TechnicalPassports", "BodyType", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "Transmission");
            DropColumn("dbo.Cars", "DriveUnit");
            DropColumn("dbo.Cars", "BodyType");
            DropColumn("dbo.TechnicalPassports", "SecondaryDamage");
            DropColumn("dbo.TechnicalPassports", "Features");
            DropColumn("dbo.TechnicalPassports", "IsMileageConfirmed");
            DropColumn("dbo.TechnicalPassports", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechnicalPassports", "Location", c => c.String());
            AddColumn("dbo.TechnicalPassports", "IsMileageConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.TechnicalPassports", "Features", c => c.String());
            AddColumn("dbo.TechnicalPassports", "SecondaryDamage", c => c.String());
            AddColumn("dbo.Cars", "BodyType", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "DriveUnit", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Transmission", c => c.Int(nullable: false));
            DropColumn("dbo.TechnicalPassports", "BodyType");
            DropColumn("dbo.TechnicalPassports", "DriveUnit");
            DropColumn("dbo.TechnicalPassports", "Transmission");
            DropColumn("dbo.TechnicalPassports", "Mileage");
        }
    }
}
