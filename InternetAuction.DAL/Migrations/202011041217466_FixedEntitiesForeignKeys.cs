namespace InternetAuction.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixedEntitiesForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bets", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "Seller_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bets", new[] { "User_Id" });
            DropIndex("dbo.Lots", new[] { "Buyer_Id" });
            DropIndex("dbo.Lots", new[] { "Seller_Id" });
            DropColumn("dbo.Bets", "UserId");
            DropColumn("dbo.Lots", "BuyerId");
            DropColumn("dbo.Lots", "SellerId");
            RenameColumn(table: "dbo.Bets", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Lots", name: "Buyer_Id", newName: "BuyerId");
            RenameColumn(table: "dbo.Lots", name: "Seller_Id", newName: "SellerId");
            AlterColumn("dbo.Bets", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bets", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Lots", "SellerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Lots", "BuyerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lots", "SellerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Bets", "UserId");
            CreateIndex("dbo.Lots", "SellerId");
            CreateIndex("dbo.Lots", "BuyerId");
            AddForeignKey("dbo.Bets", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lots", "SellerId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "SellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Lots", new[] { "BuyerId" });
            DropIndex("dbo.Lots", new[] { "SellerId" });
            DropIndex("dbo.Bets", new[] { "UserId" });
            AlterColumn("dbo.Lots", "SellerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lots", "BuyerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Lots", "SellerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bets", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Bets", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Lots", name: "SellerId", newName: "Seller_Id");
            RenameColumn(table: "dbo.Lots", name: "BuyerId", newName: "Buyer_Id");
            RenameColumn(table: "dbo.Bets", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Lots", "SellerId", c => c.Int(nullable: false));
            AddColumn("dbo.Lots", "BuyerId", c => c.Int(nullable: false));
            AddColumn("dbo.Bets", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "Seller_Id");
            CreateIndex("dbo.Lots", "Buyer_Id");
            CreateIndex("dbo.Bets", "User_Id");
            AddForeignKey("dbo.Lots", "Seller_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bets", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
