using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class TherapistConfiguration : EntityTypeConfiguration<Therapist>
    {

        #region Construction
        
        public TherapistConfiguration()
        {
            ToTable("Therapist");
            HasKey(p => p.Id);
            Property(p => p.Credentials).HasColumnName("credentials").HasMaxLength(1000);
            Property(p => p.Licenses).HasColumnName("licenses").HasMaxLength(1000);
            Property(p => p.Id).HasColumnName("therapist_id");

            HasRequired(r => r.User).WithOptional(o => o.Therapist);
        } 

        #endregion

    }

    public class TherapistPlanMatrixConfiguration : EntityTypeConfiguration<TherapistPlanMatrixItem>
    {
        #region Construction

        public TherapistPlanMatrixConfiguration()
        {
            ToTable("TherapistPlanMatrix");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.PlanId).HasColumnName("plan_id");
            Property(p => p.TherapistId).HasColumnName("therapist_id");
            Property(p => p.CanEdit).HasColumnName("can_edit");
            Property(p => p.IsOwner).HasColumnName("is_owner");
            Property(p => p.Id).HasColumnName("therapist_plan_matrix_item_id");
        }

        #endregion
    }

    public class TherapistExerciseMatrixConfiguration : EntityTypeConfiguration<TherapistExerciseMatrixItem>
    {
        #region Construction

        public TherapistExerciseMatrixConfiguration()
        {
            ToTable("TherapistExerciseMatrix");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.ExerciseId).HasColumnName("exercise_id");
            Property(p => p.TherapistId).HasColumnName("therapist_id");
            Property(p => p.CanEdit).HasColumnName("can_edit");
            Property(p => p.IsOwner).HasColumnName("is_owner");
            Property(p => p.Id).HasColumnName("therapist_exercise_matrix_item_id");
        }

        #endregion
    }
}
