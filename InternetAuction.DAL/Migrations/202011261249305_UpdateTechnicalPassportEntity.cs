namespace InternetAuction.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTechnicalPassportEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Cars", "Brand", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Cars", "EngineType", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.TechnicalPassports", "PrimaryDamage", c => c.String(maxLength: 50));
            DropColumn("dbo.TechnicalPassports", "Mileage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechnicalPassports", "Mileage", c => c.Int(nullable: false));
            AlterColumn("dbo.TechnicalPassports", "PrimaryDamage", c => c.String());
            AlterColumn("dbo.Cars", "EngineType", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
