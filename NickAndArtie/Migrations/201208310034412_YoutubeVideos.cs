namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YoutubeVideos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YoutubeVideos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Embed = c.String(),
                        IsFeatured = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YoutubeVideos");
        }
    }
}
