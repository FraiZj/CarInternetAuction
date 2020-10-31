namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCarImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Data = c.Binary(),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarImages", "CarId", "dbo.Cars");
            DropIndex("dbo.CarImages", new[] { "CarId" });
            DropTable("dbo.CarImages");
        }
    }
}
