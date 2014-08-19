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
                "Episode",
                c => new
                    {
                        episode_id = c.Long(nullable: false, identity: true),
                        episode_state_id = c.Int(nullable: false),
                        created_on = c.DateTime(nullable: false, precision: 0),
                        initial_prognosis_id = c.Int(),
                        clinic_patient_matrix_id = c.Int(nullable: false),
                        clinic_therapist_matrix_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.episode_id);
            
            CreateTable(
                "Session",
                c => new
                    {
                        session_id = c.Long(nullable: false, identity: true),
                        created_on = c.DateTime(nullable: false, precision: 0),
                        scheduled_at = c.DateTime(nullable: false, precision: 0),
                        executed_on = c.DateTime(nullable: false, precision: 0),
                        notes = c.String(unicode: false, storeType: "text"),
                        clinic_therapist_id = c.Int(nullable: false),
                        episode_id = c.Long(nullable: false),
                        differential_diagnosis_id = c.Int(),
                    })
                .PrimaryKey(t => t.session_id);
            
            CreateTable(
                "SessionPlanMatrix",
                c => new
                    {
                        session_plan_matrix_id = c.Long(nullable: false, identity: true),
                        plan_name_override = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        session_id = c.Long(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.session_plan_matrix_id);
            
            DropTable("Case");
        }
        
        public override void Down()
        {
            CreateTable(
                "Case",
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
            
            DropForeignKey("Session", "FK_dbo.Session_dbo.Episode_episode_id");
            DropForeignKey("Episode", "FK_dbo.Episode_dbo.Prognosis_initial_prognosis_id");
            DropForeignKey("Episode", "FK_dbo.Episode_dbo.ClinicTherapistMatrix_clinic_therapist_matrix_id");
            DropForeignKey("SessionPlanMatrix", "FK_dbo.SessionPlanMatrix_dbo.Session_session_id");
            DropForeignKey("SessionPlanMatrix", "FK_dbo.SessionPlanMatrix_dbo.Plan_plan_id");
            DropForeignKey("Session", "FK_dbo.Session_dbo.ClinicTherapistMatrix_clinic_therapist_id");
            DropForeignKey("Session", "FK_dbo.Session_dbo.DifferentialDiagnosis_differential_diagnosis_id");
            DropForeignKey("Episode", "FK_dbo.Episode_dbo.ClinicPatientMatrix_clinic_patient_matrix_id");
            DropIndex("Session", new[] { "episode_id" });
            DropIndex("Episode", new[] { "initial_prognosis_id" });
            DropIndex("Episode", new[] { "clinic_therapist_matrix_id" });
            DropIndex("SessionPlanMatrix", new[] { "session_id" });
            DropIndex("SessionPlanMatrix", new[] { "plan_id" });
            DropIndex("Session", new[] { "clinic_therapist_id" });
            DropIndex("Session", new[] { "differential_diagnosis_id" });
            DropIndex("Episode", new[] { "clinic_patient_matrix_id" });
            DropTable("SessionPlanMatrix");
            DropTable("Session");
            DropTable("Episode");
            CreateIndex("Case", "User_Id");
            CreateIndex("Case", "patient_id");
            CreateIndex("Case", "clinic_therapist_id");
            CreateIndex("Case", "ClinicTherapistMatrixItem_Id");
            AddForeignKey("Case", "User_Id", "User", "user_id");
            AddForeignKey("Case", "patient_id", "User", "user_id");
            AddForeignKey("Case", "clinic_therapist_id", "ClinicTherapistMatrix", "clinic_therapist_matrix_item_id");
            AddForeignKey("Case", "ClinicTherapistMatrixItem_Id", "ClinicTherapistMatrix", "clinic_therapist_matrix_item_id");
        }
    }
}
