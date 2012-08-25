namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsActive");
        }
    }
}
