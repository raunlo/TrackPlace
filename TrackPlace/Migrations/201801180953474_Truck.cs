namespace TrackPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Truck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trucks", "TypeOfTrucks", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trucks", "TypeOfTrucks", c => c.String());
        }
    }
}
