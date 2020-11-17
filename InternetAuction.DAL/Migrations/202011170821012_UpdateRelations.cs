namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lots", new[] { "SellerId" });
            AlterColumn("dbo.Lots", "SellerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Lots", "SellerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lots", new[] { "SellerId" });
            AlterColumn("dbo.Lots", "SellerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lots", "SellerId");
        }
    }
}
