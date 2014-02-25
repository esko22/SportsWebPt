namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TreatmentDescType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Treatment", "description", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Treatment", "description", c => c.String(maxLength: 200, unicode: false, storeType: "nvarchar"));
        }
    }
}
