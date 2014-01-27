using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanExerciseMatrixConfiguration : EntityTypeConfiguration<PlanExerciseMatrixItem>
    {
        #region Construction

        public PlanExerciseMatrixConfiguration()
        {
            ToTable("PlanExerciseMatrix");
            Property(p => p.PlanId).IsRequired().HasColumnName("plan_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            Property(p => p.Repititions).HasColumnName("repititions");
            Property(p => p.Order).IsRequired().HasColumnName("exercise_order");
            Property(p => p.Sets).HasColumnName("sets");
            Property(p => p.PerDay).HasColumnName("per_day");
            Property(p => p.PerWeek).HasColumnName("per_week");
            Property(p => p.HoldFor).HasColumnName("hold_for");
            Property(p => p.HoldType).HasColumnName("hold_type_id").IsRequired();
            Property(p => p.Id).IsRequired().HasColumnName("plan_exercise_matrix_id");
        }

        #endregion
    }
}
