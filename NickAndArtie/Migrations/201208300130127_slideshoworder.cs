namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slideshoworder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlideShows", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SlideShows", "Order");
        }
    }
}
