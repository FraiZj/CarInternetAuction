namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLotEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lots", "TurnkeyPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lots", "TurnkeyPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
