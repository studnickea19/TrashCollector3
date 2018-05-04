namespace TrashCollector3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyToCustomerAddressJunctionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAddresses", "Address_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.CustomerAddresses", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.CustomerAddresses", new[] { "Address_AddressID" });
            DropIndex("dbo.CustomerAddresses", new[] { "Customer_CustomerID" });
            AddColumn("dbo.CustomerAddresses", "AddressID", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerAddresses", "CustomerID", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerAddresses", "Address_AddressID");
            DropColumn("dbo.CustomerAddresses", "Customer_CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerAddresses", "Customer_CustomerID", c => c.Int());
            AddColumn("dbo.CustomerAddresses", "Address_AddressID", c => c.Int());
            DropColumn("dbo.CustomerAddresses", "CustomerID");
            DropColumn("dbo.CustomerAddresses", "AddressID");
            CreateIndex("dbo.CustomerAddresses", "Customer_CustomerID");
            CreateIndex("dbo.CustomerAddresses", "Address_AddressID");
            AddForeignKey("dbo.CustomerAddresses", "Customer_CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.CustomerAddresses", "Address_AddressID", "dbo.Addresses", "AddressID");
        }
    }
}
