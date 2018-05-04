namespace TrashCollector3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStuffBackIn : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CustomerAddresses", "AddressID");
            CreateIndex("dbo.CustomerAddresses", "CustomerID");
         
        }
        
        public override void Down()
        {
            DropIndex("dbo.CustomerAddresses", new[] { "CustomerID" });
            DropIndex("dbo.CustomerAddresses", new[] { "AddressID" });
        }
    }
}
