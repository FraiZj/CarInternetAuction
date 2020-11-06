namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLotEntity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lots", new[] { "SellerId" });
            AddColumn("dbo.Lots", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Lots", "SellerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lots", "SellerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lots", new[] { "SellerId" });
            AlterColumn("dbo.Lots", "SellerId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Lots", "IsActive");
            CreateIndex("dbo.Lots", "SellerId");
        }
    }
}
