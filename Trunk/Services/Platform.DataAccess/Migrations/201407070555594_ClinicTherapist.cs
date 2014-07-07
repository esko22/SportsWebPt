namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicTherapist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicAdmin",
                c => new
                    {
                        clinic_admin_id = c.Int(nullable: false),
                        emergency_contact = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.clinic_admin_id)
                .ForeignKey("dbo.User", t => t.clinic_admin_id)
                .Index(t => t.clinic_admin_id);
            
            CreateTable(
                "dbo.ClinicAdminMatrix",
                c => new
                    {
                        clinic_admin_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        clinic_admin_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_admin_matrix_item_id)
                .ForeignKey("dbo.ClinicAdmin", t => t.clinic_admin_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .Index(t => t.clinic_admin_id)
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
                        therapist_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_therapist_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.Therapist", t => t.therapist_id)
                .Index(t => t.clinic_id)
                .Index(t => t.therapist_id);
            
            CreateTable(
                "dbo.Therapist",
                c => new
                    {
                        therapist_id = c.Int(nullable: false),
                        credentials = c.String(maxLength: 1000, unicode: false, storeType: "nvarchar"),
                        licenses = c.String(maxLength: 1000, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.therapist_id)
                .ForeignKey("dbo.User", t => t.therapist_id)
                .Index(t => t.therapist_id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClinicAdmin", "clinic_admin_id", "dbo.User");
            DropForeignKey("dbo.Location", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.Therapist", "therapist_id", "dbo.User");
            DropForeignKey("dbo.ClinicTherapistMatrix", "therapist_id", "dbo.Therapist");
            DropForeignKey("dbo.ClinicTherapistMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicPatientMatrix", "user_id", "dbo.User");
            DropForeignKey("dbo.ClinicPatientMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicAdminMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicAdminMatrix", "clinic_admin_id", "dbo.ClinicAdmin");
            DropIndex("dbo.ClinicAdmin", new[] { "clinic_admin_id" });
            DropIndex("dbo.Location", new[] { "clinic_id" });
            DropIndex("dbo.Therapist", new[] { "therapist_id" });
            DropIndex("dbo.ClinicTherapistMatrix", new[] { "therapist_id" });
            DropIndex("dbo.ClinicTherapistMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicPatientMatrix", new[] { "user_id" });
            DropIndex("dbo.ClinicPatientMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicAdminMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicAdminMatrix", new[] { "clinic_admin_id" });
            DropTable("dbo.Location");
            DropTable("dbo.Therapist");
            DropTable("dbo.ClinicTherapistMatrix");
            DropTable("dbo.ClinicPatientMatrix");
            DropTable("dbo.Clinic");
            DropTable("dbo.ClinicAdminMatrix");
            DropTable("dbo.ClinicAdmin");
        }
    }
}
