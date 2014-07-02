namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicAdminMatrix",
                c => new
                    {
                        clinic_admin_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_admin_matrix_item_id)
                .ForeignKey("dbo.User", t => t.user_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .Index(t => t.user_id)
                .Index(t => t.clinic_id);
            
            CreateTable(
                "dbo.Clinic",
                c => new
                    {
                        clinic_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 200, unicode: false, storeType: "nvarchar"),
                        display_image = c.String(maxLength: 1000, unicode: false, storeType: "nvarchar"),
                        phone = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        website = c.String(maxLength: 200, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.clinic_id);
            
            CreateTable(
                "dbo.ClinicPatientMatrix",
                c => new
                    {
                        clinic_patient_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_patient_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.User", t => t.user_id)
                .Index(t => t.clinic_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.ClinicTherapistMatrix",
                c => new
                    {
                        clinic_therapist_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_therapist_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.User", t => t.user_id)
                .Index(t => t.clinic_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        location_id = c.Long(nullable: false, identity: true),
                        address = c.String(maxLength: 500, unicode: false, storeType: "nvarchar"),
                        city = c.String(maxLength: 500, unicode: false, storeType: "nvarchar"),
                        state = c.String(maxLength: 2, unicode: false, storeType: "nvarchar"),
                        zipcode = c.Int(nullable: false),
                        clinic_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.location_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .Index(t => t.clinic_id);
            
            AddColumn("dbo.User", "credentials", c => c.String(maxLength: 1000, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.User", "licenses", c => c.String(maxLength: 1000, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Location", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicTherapistMatrix", "user_id", "dbo.User");
            DropForeignKey("dbo.ClinicTherapistMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicPatientMatrix", "user_id", "dbo.User");
            DropForeignKey("dbo.ClinicPatientMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicAdminMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicAdminMatrix", "user_id", "dbo.User");
            DropIndex("dbo.Location", new[] { "clinic_id" });
            DropIndex("dbo.ClinicTherapistMatrix", new[] { "user_id" });
            DropIndex("dbo.ClinicTherapistMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicPatientMatrix", new[] { "user_id" });
            DropIndex("dbo.ClinicPatientMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicAdminMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicAdminMatrix", new[] { "user_id" });
            DropColumn("dbo.User", "licenses");
            DropColumn("dbo.User", "credentials");
            DropTable("dbo.Location");
            DropTable("dbo.ClinicTherapistMatrix");
            DropTable("dbo.ClinicPatientMatrix");
            DropTable("dbo.Clinic");
            DropTable("dbo.ClinicAdminMatrix");
        }
    }
}
