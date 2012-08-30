namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slidehsows : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SlideShows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageLarge = c.String(),
                        ImageDestination = c.String(),
                        IsListenLive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SlideShows");
        }
    }
}
