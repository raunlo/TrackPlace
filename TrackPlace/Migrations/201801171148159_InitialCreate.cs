namespace TrackPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        BillNumber = c.Int(nullable: false),
                        BillDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Price = c.Int(nullable: false),
                        UserAccontId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.UserAcconts", t => t.UserAccontId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.UserAccontId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        LoadingCounty = c.String(nullable: false),
                        LoadingAddress = c.String(nullable: false),
                        LoadingDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoadingCity = c.String(),
                        LoadingHouseNumber = c.Int(nullable: false),
                        UnloadingCounty = c.String(nullable: false),
                        UnloadingAddress = c.String(nullable: false),
                        UnloadingCity = c.String(),
                        UnloadingHouseNumber = c.Int(nullable: false),
                        UnloadingDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProductName = c.String(nullable: false),
                        ProductLength = c.Int(),
                        ProductWidth = c.Int(),
                        ProductHeight = c.Int(),
                        ProductWeight = c.Int(nullable: false),
                        ProductCubage = c.Int(),
                        UserAccontId = c.Int(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.UserAcconts", t => t.UserAccontId)
                .Index(t => t.UserAccontId);
            
            CreateTable(
                "dbo.TruckInOrders",
                c => new
                    {
                        TruckInOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        TruckId = c.Int(nullable: false),
                        TypeOfTrucks = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TruckInOrderId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Trucks", t => t.TruckId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.TruckId);
            
            CreateTable(
                "dbo.Trucks",
                c => new
                    {
                        TruckId = c.Int(nullable: false, identity: true),
                        BodyType = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Make = c.String(nullable: false),
                        FirstRegDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UnladenWeigth = c.Int(nullable: false),
                        MaximumWeight = c.Int(nullable: false),
                        WeigthCapacity = c.Int(nullable: false),
                        TrailerLength = c.Int(nullable: false),
                        RegistrationNumber = c.String(nullable: false),
                        TrailerWidth = c.Int(nullable: false),
                        TrailerHeight = c.Int(nullable: false),
                        Fuel = c.String(nullable: false),
                        VinCode = c.String(nullable: false),
                        TrailerCubage = c.Int(nullable: false),
                        UserAccontId = c.Int(nullable: false),
                        TypeOfTrucks = c.String(),
                    })
                .PrimaryKey(t => t.TruckId)
                .ForeignKey("dbo.UserAcconts", t => t.UserAccontId, cascadeDelete: true)
                .Index(t => t.UserAccontId);
            
            CreateTable(
                "dbo.UserAcconts",
                c => new
                    {
                        UserAccontId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        PasswordId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserAccontId)
                .ForeignKey("dbo.Passwords", t => t.PasswordId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.PasswordId);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        PasswordId = c.Int(nullable: false, identity: true),
                        PasswordName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PasswordId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EMailAddress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Trucks", "UserAccontId", "dbo.UserAcconts");
            DropForeignKey("dbo.UserAcconts", "PersonId", "dbo.People");
            DropForeignKey("dbo.UserAcconts", "PasswordId", "dbo.Passwords");
            DropForeignKey("dbo.Orders", "UserAccontId", "dbo.UserAcconts");
            DropForeignKey("dbo.Bills", "UserAccontId", "dbo.UserAcconts");
            DropForeignKey("dbo.TruckInOrders", "TruckId", "dbo.Trucks");
            DropForeignKey("dbo.TruckInOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.UserAcconts", new[] { "PasswordId" });
            DropIndex("dbo.UserAcconts", new[] { "PersonId" });
            DropIndex("dbo.Trucks", new[] { "UserAccontId" });
            DropIndex("dbo.TruckInOrders", new[] { "TruckId" });
            DropIndex("dbo.TruckInOrders", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserAccontId" });
            DropIndex("dbo.Bills", new[] { "OrderId" });
            DropIndex("dbo.Bills", new[] { "UserAccontId" });
            DropTable("dbo.People");
            DropTable("dbo.Passwords");
            DropTable("dbo.UserAcconts");
            DropTable("dbo.Trucks");
            DropTable("dbo.TruckInOrders");
            DropTable("dbo.Orders");
            DropTable("dbo.Bills");
        }
    }
}
