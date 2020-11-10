namespace InternetAuction.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false));
        }
    }
}
