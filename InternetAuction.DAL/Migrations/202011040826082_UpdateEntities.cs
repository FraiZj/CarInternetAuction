namespace InternetAuction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "BodyTypeId", "dbo.BodyTypes");
            DropForeignKey("dbo.Cars", "DriveUnitId", "dbo.DriveUnits");
            DropForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.Lots", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.Lots", new[] { "SaleTypeId" });
            DropIndex("dbo.Cars", new[] { "TransmissionId" });
            DropIndex("dbo.Cars", new[] { "DriveUnitId" });
            DropIndex("dbo.Cars", new[] { "BodyTypeId" });
            AddColumn("dbo.Lots", "SaleType", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Transmission", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "DriveUnit", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "BodyType", c => c.Int(nullable: false));
            DropColumn("dbo.Lots", "SaleTypeId");
            DropColumn("dbo.Cars", "TransmissionId");
            DropColumn("dbo.Cars", "DriveUnitId");
            DropColumn("dbo.Cars", "BodyTypeId");
            DropTable("dbo.BodyTypes");
            DropTable("dbo.DriveUnits");
            DropTable("dbo.Transmissions");
            DropTable("dbo.SaleTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SaleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transmissions",
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
                "dbo.BodyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "BodyTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "DriveUnitId", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "TransmissionId", c => c.Int(nullable: false));
            AddColumn("dbo.Lots", "SaleTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "BodyType");
            DropColumn("dbo.Cars", "DriveUnit");
            DropColumn("dbo.Cars", "Transmission");
            DropColumn("dbo.Lots", "SaleType");
            CreateIndex("dbo.Cars", "BodyTypeId");
            CreateIndex("dbo.Cars", "DriveUnitId");
            CreateIndex("dbo.Cars", "TransmissionId");
            CreateIndex("dbo.Lots", "SaleTypeId");
            AddForeignKey("dbo.Lots", "SaleTypeId", "dbo.SaleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "DriveUnitId", "dbo.DriveUnits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "BodyTypeId", "dbo.BodyTypes", "Id", cascadeDelete: true);
        }
    }
}
