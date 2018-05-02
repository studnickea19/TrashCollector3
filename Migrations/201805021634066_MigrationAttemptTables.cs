namespace TrashCollector3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAttemptTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        ChargeID = c.Int(nullable: false, identity: true),
                        ChargeAmount = c.Double(nullable: false),
                        PickupID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChargeID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Pickups", t => t.PickupID, cascadeDelete: true)
                .Index(t => t.PickupID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerPassword = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        PickupID = c.Int(nullable: false),
                        CustomerAddressID = c.Int(nullable: false),
                        PickUpDate = c.DateTime(nullable: false),
                        AreaID = c.Int(nullable: false),
                        PickupCompleted = c.Boolean(nullable: false),
                        PickupSuspended = c.Boolean(nullable: false),
                        OneTimePickup = c.Boolean(nullable: false),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.PickupID)
                .ForeignKey("dbo.CustomerAddresses", t => t.CustomerAddressID, cascadeDelete: true)
                .ForeignKey("dbo.PickUpAreas", t => t.AreaID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .ForeignKey("dbo.Employees", t => t.PickupID)
                .Index(t => t.PickupID)
                .Index(t => t.CustomerAddressID)
                .Index(t => t.AreaID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.CustomerAddresses",
                c => new
                    {
                        CustomerAddressID = c.Int(nullable: false, identity: true),
                        Address_AddressID = c.Int(),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerAddressID)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .Index(t => t.Address_AddressID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.PickUpAreas",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AreaID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeFirstName = c.String(),
                        EmployeeLastName = c.String(),
                        AreaID = c.Int(nullable: false),
                        PickupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.PickUpAreas", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "AreaID", "dbo.PickUpAreas");
            DropForeignKey("dbo.Pickups", "PickupID", "dbo.Employees");
            DropForeignKey("dbo.Charges", "PickupID", "dbo.Pickups");
            DropForeignKey("dbo.Addresses", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Pickups", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Pickups", "AreaID", "dbo.PickUpAreas");
            DropForeignKey("dbo.Pickups", "CustomerAddressID", "dbo.CustomerAddresses");
            DropForeignKey("dbo.CustomerAddresses", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.CustomerAddresses", "Address_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Charges", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "AreaID" });
            DropIndex("dbo.CustomerAddresses", new[] { "Customer_CustomerID" });
            DropIndex("dbo.CustomerAddresses", new[] { "Address_AddressID" });
            DropIndex("dbo.Pickups", new[] { "Customer_CustomerID" });
            DropIndex("dbo.Pickups", new[] { "AreaID" });
            DropIndex("dbo.Pickups", new[] { "CustomerAddressID" });
            DropIndex("dbo.Pickups", new[] { "PickupID" });
            DropIndex("dbo.Charges", new[] { "CustomerID" });
            DropIndex("dbo.Charges", new[] { "PickupID" });
            DropIndex("dbo.Addresses", new[] { "Customer_CustomerID" });
            DropTable("dbo.Employees");
            DropTable("dbo.PickUpAreas");
            DropTable("dbo.CustomerAddresses");
            DropTable("dbo.Pickups");
            DropTable("dbo.Customers");
            DropTable("dbo.Charges");
            DropTable("dbo.Addresses");
        }
    }
}
