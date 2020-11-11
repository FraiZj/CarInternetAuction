namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "StartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Lots", "TurnkeyPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Lots", "AuctionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "AuctionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Lots", "TurnkeyPrice");
            DropColumn("dbo.Lots", "StartPrice");
        }
    }
}
