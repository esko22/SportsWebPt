namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Session", "session_type_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Session", "session_type_id");
        }
    }
}
