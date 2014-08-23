namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Episodes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Case", "ClinicTherapistMatrixItem_Id", "ClinicTherapistMatrix");
            DropForeignKey("Case", "clinic_therapist_id", "ClinicTherapistMatrix");
            DropForeignKey("Case", "patient_id", "User");
            DropForeignKey("Case", "User_Id", "User");
            DropIndex("Case", new[] { "ClinicTherapistMatrixItem_Id" });
            DropIndex("Case", new[] { "clinic_therapist_id" });
            DropIndex("Case", new[] { "patient_id" });
            DropIndex("Case", new[] { "User_Id" });
            CreateTable(
                "dbo.Episode",
                c => new
                    {
                        episode_id = c.Long(nullable: false, identity: true),
                        episode_state_id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        description = c.String(unicode: false, storeType: "text"),
                        created_on = c.DateTime(nullable: false, precision: 0),
                        initial_prognosis_id = c.Int(),
                        patient_id = c.Int(nullable: false),
                        therapist_id = c.Int(nullable: false),
                        clinic_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.episode_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.User", t => t.patient_id)
                .ForeignKey("dbo.Prognosis", t => t.initial_prognosis_id)
                .ForeignKey("dbo.Therapist", t => t.therapist_id)
                .Index(t => t.clinic_id)
                .Index(t => t.patient_id)
                .Index(t => t.initial_prognosis_id)
                .Index(t => t.therapist_id);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        session_id = c.Long(nullable: false, identity: true),
                        created_on = c.DateTime(nullable: false, precision: 0),
                        scheduled_at = c.DateTime(nullable: false, precision: 0),
                        executed_on = c.DateTime(nullable: false, precision: 0),
                        notes = c.String(unicode: false, storeType: "text"),
                        therapist_id = c.Int(nullable: false),
                        episode_id = c.Long(nullable: false),
                        differential_diagnosis_id = c.Int(),
                    })
                .PrimaryKey(t => t.session_id)
                .ForeignKey("dbo.DifferentialDiagnosis", t => t.differential_diagnosis_id)
                .ForeignKey("dbo.Therapist", t => t.therapist_id)
                .ForeignKey("dbo.Episode", t => t.episode_id)
                .Index(t => t.differential_diagnosis_id)
                .Index(t => t.therapist_id)
                .Index(t => t.episode_id);
            
            CreateTable(
                "dbo.SessionPlanMatrix",
                c => new
                    {
                        session_plan_matrix_id = c.Long(nullable: false, identity: true),
                        plan_name_override = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        session_id = c.Long(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.session_plan_matrix_id)
                .ForeignKey("dbo.Plan", t => t.plan_id)
                .ForeignKey("dbo.Session", t => t.session_id)
                .Index(t => t.plan_id)
                .Index(t => t.session_id);
            
            DropTable("dbo.Case");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Case",
                c => new
                    {
                        case_id = c.Int(nullable: false, identity: true),
                        patient_id = c.Int(nullable: false),
                        clinic_therapist_id = c.Int(nullable: false),
                        is_active = c.Boolean(nullable: false),
                        create_date = c.DateTime(nullable: false, precision: 0),
                        name = c.String(maxLength: 500, unicode: false, storeType: "nvarchar"),
                        ClinicTherapistMatrixItem_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.case_id);
            
            DropForeignKey("dbo.Episode", "therapist_id", "dbo.Therapist");
            DropForeignKey("dbo.Session", "episode_id", "dbo.Episode");
            DropForeignKey("dbo.Session", "therapist_id", "dbo.Therapist");
            DropForeignKey("dbo.SessionPlanMatrix", "session_id", "dbo.Session");
            DropForeignKey("dbo.SessionPlanMatrix", "plan_id", "dbo.Plan");
            DropForeignKey("dbo.Session", "differential_diagnosis_id", "dbo.DifferentialDiagnosis");
            DropForeignKey("dbo.Episode", "initial_prognosis_id", "dbo.Prognosis");
            DropForeignKey("dbo.Episode", "patient_id", "dbo.User");
            DropForeignKey("dbo.Episode", "clinic_id", "dbo.Clinic");
            DropIndex("dbo.Episode", new[] { "therapist_id" });
            DropIndex("dbo.Session", new[] { "episode_id" });
            DropIndex("dbo.Session", new[] { "therapist_id" });
            DropIndex("dbo.SessionPlanMatrix", new[] { "session_id" });
            DropIndex("dbo.SessionPlanMatrix", new[] { "plan_id" });
            DropIndex("dbo.Session", new[] { "differential_diagnosis_id" });
            DropIndex("dbo.Episode", new[] { "initial_prognosis_id" });
            DropIndex("dbo.Episode", new[] { "patient_id" });
            DropIndex("dbo.Episode", new[] { "clinic_id" });
            DropTable("dbo.SessionPlanMatrix");
            DropTable("dbo.Session");
            DropTable("dbo.Episode");
            CreateIndex("dbo.Case", "User_Id");
            CreateIndex("dbo.Case", "patient_id");
            CreateIndex("dbo.Case", "clinic_therapist_id");
            CreateIndex("dbo.Case", "ClinicTherapistMatrixItem_Id");
            AddForeignKey("dbo.Case", "User_Id", "dbo.User", "user_id");
            AddForeignKey("dbo.Case", "patient_id", "dbo.User", "user_id");
            AddForeignKey("dbo.Case", "clinic_therapist_id", "dbo.ClinicTherapistMatrix", "clinic_therapist_matrix_item_id");
            AddForeignKey("dbo.Case", "ClinicTherapistMatrixItem_Id", "dbo.ClinicTherapistMatrix", "clinic_therapist_matrix_item_id");
        }
    }
}
