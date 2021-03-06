namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerEntities2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "UserExercise", newName: "ExerciseUser");
            RenameTable(name: "UserVideo", newName: "VideoUser");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.VideoUser", newName: "UserVideo");
            RenameTable(name: "dbo.ExerciseUser", newName: "UserExercise");
        }
    }
}
