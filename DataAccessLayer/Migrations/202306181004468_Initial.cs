namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(maxLength: 50),
                        ActivityInfo = c.String(maxLength: 500),
                        ActivityPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.BookedActivities",
                c => new
                    {
                        BookedActivityId = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                        ReservationKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookedActivityId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.ActivityId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 50),
                        Mail = c.String(maxLength: 100),
                        Password = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.BookedReservations",
                c => new
                    {
                        BookedReservationId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservationKey = c.Int(nullable: false),
                        NrOfCustomers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookedReservationId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationName = c.String(maxLength: 50),
                        ReservationInfo = c.String(maxLength: 1500),
                        ImageURL = c.String(maxLength: 150),
                        ReservationNr = c.Int(nullable: false),
                        SoldReservationNr = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ReservationPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Rating1 = c.Int(nullable: false),
                        Rating2 = c.Int(nullable: false),
                        Rating3 = c.Int(nullable: false),
                        Rating4 = c.Int(nullable: false),
                        Rating5 = c.Int(nullable: false),
                        Rating6 = c.Int(nullable: false),
                        Rating7 = c.Int(nullable: false),
                        Rating8 = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Admins", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BookedReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.BookedReservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BookedActivities", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BookedActivities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.BookedReservations", new[] { "CustomerId" });
            DropIndex("dbo.BookedReservations", new[] { "ReservationId" });
            DropIndex("dbo.BookedActivities", new[] { "CustomerId" });
            DropIndex("dbo.BookedActivities", new[] { "ActivityId" });
            DropColumn("dbo.Admins", "Name");
            DropTable("dbo.Ratings");
            DropTable("dbo.Reservations");
            DropTable("dbo.BookedReservations");
            DropTable("dbo.Customers");
            DropTable("dbo.BookedActivities");
            DropTable("dbo.Activities");
        }
    }
}
