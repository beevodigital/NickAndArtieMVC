namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Podcasts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Podcasts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Image = c.String(),
                        Artist = c.String(),
                        Title = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DatePublished = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Podcasts");
        }
    }
}
