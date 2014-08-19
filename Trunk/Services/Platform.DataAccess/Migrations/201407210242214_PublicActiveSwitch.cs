namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublicActiveSwitch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClinicInjuryMatrix", "is_active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClinicExerciseMatrix", "is_active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClinicPlanMatrix", "is_active", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlanPublishDetail", "visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExercisePublishDetail", "visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.InjuryPublishDetail", "visible", c => c.Boolean(nullable: false));
            DropColumn("dbo.ClinicInjuryMatrix", "is_public");
            DropColumn("dbo.ClinicExerciseMatrix", "is_public");
            DropColumn("dbo.ClinicPlanMatrix", "is_public");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClinicPlanMatrix", "is_public", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClinicExerciseMatrix", "is_public", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClinicInjuryMatrix", "is_public", c => c.Boolean(nullable: false));
            DropColumn("dbo.InjuryPublishDetail", "visible");
            DropColumn("dbo.ExercisePublishDetail", "visible");
            DropColumn("dbo.PlanPublishDetail", "visible");
            DropColumn("dbo.ClinicPlanMatrix", "is_active");
            DropColumn("dbo.ClinicExerciseMatrix", "is_active");
            DropColumn("dbo.ClinicInjuryMatrix", "is_active");
        }
    }
}
