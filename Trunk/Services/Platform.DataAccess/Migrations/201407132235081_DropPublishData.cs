namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPublishData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO planpublishdetail(plan_id,page_name,tags) SELECT plan_id, page_name,tags from plan");
            Sql("INSERT INTO clinicplanmatrix (clinic_id,plan_id,is_public) SELECT 2, plan_id, 1 FROM plan");
            Sql("INSERT INTO injurypublishdetail (injury_id,page_name,tags,opening_statement) SELECT injury_id, page_name,tags,opening_statement FROM injury");
            Sql("INSERT INTO clinicinjurymatrix (clinic_id,injury_id,is_public) SELECT 2, injury_id, 1 FROM injury");
            Sql("INSERT INTO exercisepublishdetail (exercise_id,page_name,tags) SELECT exercise_id, page_name,tags FROM exercise");
            Sql("INSERT INTO clinicexercisematrix (clinic_id,exercise_id,is_public) SELECT 2, exercise_id, 1 FROM exercise");

            DropColumn("dbo.Injury", "opening_statement");
            DropColumn("dbo.Injury", "page_name");
            DropColumn("dbo.Injury", "tags");
            DropColumn("dbo.Plan", "page_name");
            DropColumn("dbo.Plan", "tags");
            DropColumn("dbo.Exercise", "page_name");
            DropColumn("dbo.Exercise", "tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercise", "tags", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Exercise", "page_name", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Plan", "tags", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Plan", "page_name", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Injury", "tags", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Injury", "page_name", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Injury", "opening_statement", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
    }
}
