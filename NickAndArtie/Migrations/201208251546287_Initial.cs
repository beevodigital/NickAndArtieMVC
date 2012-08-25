namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeadingShort = c.String(),
                        AirDate = c.DateTime(nullable: false),
                        DescriptionShort = c.String(),
                        DescriptionLong = c.String(),
                        ImageLarge = c.String(),
                        ImageThumb = c.String(),
                        HeadingLong = c.String(),
                        HeadingSub = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}
