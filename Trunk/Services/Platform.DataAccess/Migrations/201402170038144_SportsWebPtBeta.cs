namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SportsWebPtBeta : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.VideoUser", newName: "UserVideo");
            //RenameTable(name: "dbo.ExerciseUser", newName: "UserExercise");
            //RenameTable(name: "dbo.PlanUser", newName: "UserPlan");
            //RenameTable(name: "dbo.InjuryUser", newName: "UserInjury");
            //AlterColumn("dbo.Video", "created_on", c => c.DateTime(nullable: false, precision: 0));
            //AlterColumn("dbo.DifferentialDiagnosis", "submitted_on", c => c.DateTime(nullable: false, precision: 0));
            //AlterColumn("dbo.DifferentialDiagnosis", "reviewed_on", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.DifferentialDiagnosis", "reviewed_on", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.DifferentialDiagnosis", "submitted_on", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Video", "created_on", c => c.DateTime(nullable: false));
            //RenameTable(name: "dbo.UserInjury", newName: "InjuryUser");
            //RenameTable(name: "dbo.UserPlan", newName: "PlanUser");
            //RenameTable(name: "dbo.UserExercise", newName: "ExerciseUser");
            //RenameTable(name: "dbo.UserVideo", newName: "VideoUser");
        }
    }
}
