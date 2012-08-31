namespace NickAndArtie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoadTrips", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoadTrips", "URL");
        }
    }
}
