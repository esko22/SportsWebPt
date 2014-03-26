namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InjurySymptomMatrix", "is_red_flag", c => c.Boolean(nullable: false, storeType: "bit"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InjurySymptomMatrix", "is_red_flag");
        }
    }
}
