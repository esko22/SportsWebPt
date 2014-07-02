namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseStructures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercise", "structures_involved", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercise", "structures_involved");
        }
    }
}
