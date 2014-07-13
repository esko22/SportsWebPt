namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerEntities : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.case_id)
                .ForeignKey("dbo.ClinicTherapistMatrix", t => t.ClinicTherapistMatrixItem_Id)
                .ForeignKey("dbo.ClinicTherapistMatrix", t => t.clinic_therapist_id)
                .ForeignKey("dbo.User", t => t.patient_id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.ClinicTherapistMatrixItem_Id)
                .Index(t => t.clinic_therapist_id)
                .Index(t => t.patient_id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ClinicExerciseMatrix",
                c => new
                    {
                        clinic_exercise_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        exercise_id = c.Int(nullable: false),
                        is_public = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_exercise_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.Exercise", t => t.exercise_id)
                .Index(t => t.clinic_id)
                .Index(t => t.exercise_id);
            
            CreateTable(
                "dbo.ClinicPlanMatrix",
                c => new
                    {
                        clinic_plan_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                        is_public = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_plan_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.Plan", t => t.plan_id)
                .Index(t => t.clinic_id)
                .Index(t => t.plan_id);
            
            CreateTable(
                "dbo.TherapistExerciseMatrix",
                c => new
                    {
                        therapist_exercise_matrix_item_id = c.Int(nullable: false, identity: true),
                        therapist_id = c.Int(nullable: false),
                        exercise_id = c.Int(nullable: false),
                        is_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.therapist_exercise_matrix_item_id)
                .ForeignKey("dbo.Exercise", t => t.exercise_id)
                .ForeignKey("dbo.Therapist", t => t.therapist_id)
                .Index(t => t.exercise_id)
                .Index(t => t.therapist_id);
            
            CreateTable(
                "dbo.TherapistPlanMatrix",
                c => new
                    {
                        therapist_plan_matrix_item_id = c.Int(nullable: false, identity: true),
                        therapist_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                        is_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.therapist_plan_matrix_item_id)
                .ForeignKey("dbo.Plan", t => t.plan_id)
                .ForeignKey("dbo.Therapist", t => t.therapist_id)
                .Index(t => t.plan_id)
                .Index(t => t.therapist_id);
            
            CreateTable(
                "dbo.ExercisePublishDetail",
                c => new
                    {
                        exercise_id = c.Int(nullable: false),
                        page_name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        tags = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.exercise_id)
                .ForeignKey("dbo.Exercise", t => t.exercise_id)
                .Index(t => t.exercise_id);
            
            CreateTable(
                "dbo.PlanPublishDetail",
                c => new
                    {
                        plan_id = c.Int(nullable: false),
                        page_name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        tags = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.plan_id)
                .ForeignKey("dbo.Plan", t => t.plan_id)
                .Index(t => t.plan_id);
            
            CreateTable(
                "dbo.InjuryPublishDetail",
                c => new
                    {
                        injury_id = c.Int(nullable: false),
                        page_name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        tags = c.String(unicode: false, storeType: "text"),
                        opening_statement = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.injury_id)
                .ForeignKey("dbo.Injury", t => t.injury_id)
                .Index(t => t.injury_id);
            
            CreateTable(
                "dbo.ClinicInjuryMatrix",
                c => new
                    {
                        clinic_injury_matrix_item_id = c.Int(nullable: false, identity: true),
                        clinic_id = c.Int(nullable: false),
                        injury_id = c.Int(nullable: false),
                        is_public = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.clinic_injury_matrix_item_id)
                .ForeignKey("dbo.Clinic", t => t.clinic_id)
                .ForeignKey("dbo.Injury", t => t.injury_id)
                .Index(t => t.clinic_id)
                .Index(t => t.injury_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClinicInjuryMatrix", "injury_id", "dbo.Injury");
            DropForeignKey("dbo.ClinicInjuryMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.InjuryPublishDetail", "injury_id", "dbo.Injury");
            DropForeignKey("dbo.PlanPublishDetail", "plan_id", "dbo.Plan");
            DropForeignKey("dbo.ExercisePublishDetail", "exercise_id", "dbo.Exercise");
            DropForeignKey("dbo.Case", "User_Id", "dbo.User");
            DropForeignKey("dbo.Case", "patient_id", "dbo.User");
            DropForeignKey("dbo.Case", "clinic_therapist_id", "dbo.ClinicTherapistMatrix");
            DropForeignKey("dbo.TherapistPlanMatrix", "therapist_id", "dbo.Therapist");
            DropForeignKey("dbo.TherapistPlanMatrix", "plan_id", "dbo.Plan");
            DropForeignKey("dbo.TherapistExerciseMatrix", "therapist_id", "dbo.Therapist");
            DropForeignKey("dbo.TherapistExerciseMatrix", "exercise_id", "dbo.Exercise");
            DropForeignKey("dbo.ClinicPlanMatrix", "plan_id", "dbo.Plan");
            DropForeignKey("dbo.ClinicPlanMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.ClinicExerciseMatrix", "exercise_id", "dbo.Exercise");
            DropForeignKey("dbo.ClinicExerciseMatrix", "clinic_id", "dbo.Clinic");
            DropForeignKey("dbo.Case", "ClinicTherapistMatrixItem_Id", "dbo.ClinicTherapistMatrix");
            DropIndex("dbo.ClinicInjuryMatrix", new[] { "injury_id" });
            DropIndex("dbo.ClinicInjuryMatrix", new[] { "clinic_id" });
            DropIndex("dbo.InjuryPublishDetail", new[] { "injury_id" });
            DropIndex("dbo.PlanPublishDetail", new[] { "plan_id" });
            DropIndex("dbo.ExercisePublishDetail", new[] { "exercise_id" });
            DropIndex("dbo.Case", new[] { "User_Id" });
            DropIndex("dbo.Case", new[] { "patient_id" });
            DropIndex("dbo.Case", new[] { "clinic_therapist_id" });
            DropIndex("dbo.TherapistPlanMatrix", new[] { "therapist_id" });
            DropIndex("dbo.TherapistPlanMatrix", new[] { "plan_id" });
            DropIndex("dbo.TherapistExerciseMatrix", new[] { "therapist_id" });
            DropIndex("dbo.TherapistExerciseMatrix", new[] { "exercise_id" });
            DropIndex("dbo.ClinicPlanMatrix", new[] { "plan_id" });
            DropIndex("dbo.ClinicPlanMatrix", new[] { "clinic_id" });
            DropIndex("dbo.ClinicExerciseMatrix", new[] { "exercise_id" });
            DropIndex("dbo.ClinicExerciseMatrix", new[] { "clinic_id" });
            DropIndex("dbo.Case", new[] { "ClinicTherapistMatrixItem_Id" });
            DropTable("dbo.ClinicInjuryMatrix");
            DropTable("dbo.InjuryPublishDetail");
            DropTable("dbo.PlanPublishDetail");
            DropTable("dbo.ExercisePublishDetail");
            DropTable("dbo.TherapistPlanMatrix");
            DropTable("dbo.TherapistExerciseMatrix");
            DropTable("dbo.ClinicPlanMatrix");
            DropTable("dbo.ClinicExerciseMatrix");
            DropTable("dbo.Case");
        }
    }
}
