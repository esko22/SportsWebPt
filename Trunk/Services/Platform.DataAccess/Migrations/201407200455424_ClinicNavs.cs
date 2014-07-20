namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicNavs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "UserPlan", newName: "PlanUser");
        }
        
        public override void Down()
        {
            RenameTable(name: "PlanUser", newName: "UserPlan");
        }
    }
}
