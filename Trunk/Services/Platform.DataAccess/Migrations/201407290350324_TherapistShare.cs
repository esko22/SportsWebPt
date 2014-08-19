namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TherapistShare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TherapistExerciseMatrix", "is_owner", c => c.Boolean(nullable: false));
            AddColumn("dbo.TherapistExerciseMatrix", "can_edit", c => c.Boolean(nullable: false));
            AddColumn("dbo.TherapistPlanMatrix", "is_owner", c => c.Boolean(nullable: false));
            AddColumn("dbo.TherapistPlanMatrix", "can_edit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TherapistPlanMatrix", "can_edit");
            DropColumn("dbo.TherapistPlanMatrix", "is_owner");
            DropColumn("dbo.TherapistExerciseMatrix", "can_edit");
            DropColumn("dbo.TherapistExerciseMatrix", "is_owner");
        }
    }
}
