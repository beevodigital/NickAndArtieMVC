namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoReels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoReels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageThumb = c.String(),
                        ImageLarge = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoadTrips",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOfEvent = c.DateTime(nullable: false),
                        Location = c.String(),
                        Artist = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoadTrips");
            DropTable("dbo.PhotoReels");
        }
    }
}
