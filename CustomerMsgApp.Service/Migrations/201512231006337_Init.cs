namespace CustomerMsgApp.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailLogReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationData",
                c => new
                    {
                        BookingRef = c.Long(nullable: false, identity: true),
                        TourOpCode = c.String(maxLength: 255),
                        PassengerId = c.Int(nullable: false),
                        Title = c.String(maxLength: 255),
                        FirstName = c.String(maxLength: 255),
                        Surname = c.String(maxLength: 255),
                        MobileNo = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        DirectOrAgent = c.String(maxLength: 255),
                        StartDate = c.DateTime(nullable: false),
                        DeparturePoint = c.String(maxLength: 255),
                        ArrivalPoint = c.String(maxLength: 255),
                        TravelDate = c.DateTime(),
                        TravelDepatureTime = c.String(maxLength: 255),
                        TravelArrivalTime = c.String(maxLength: 255),
                        TravelDirection = c.String(maxLength: 255),
                        TransportCarrier = c.String(maxLength: 255),
                        TransportNumber = c.String(maxLength: 255),
                        TransportType = c.String(maxLength: 255),
                        TransportChain = c.String(maxLength: 255),
                        CountryCode = c.String(maxLength: 255),
                        CountryName = c.String(maxLength: 255),
                        ResortCode = c.String(maxLength: 255),
                        ResortName = c.String(maxLength: 255),
                        AccommodationCode = c.String(maxLength: 255),
                        AccommodationName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.BookingRef);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NotificationData");
            DropTable("dbo.EmailLogReport");
        }
    }
}
