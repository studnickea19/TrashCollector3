namespace TrashCollector3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUpAreas", "Zipcodes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUpAreas", "Zipcodes");
        }
    }
}
