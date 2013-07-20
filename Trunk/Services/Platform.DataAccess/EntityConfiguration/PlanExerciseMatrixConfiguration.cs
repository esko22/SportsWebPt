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
            Property(p => p.Sets).HasColumnName("sets");
            Property(p => p.Id).IsRequired().HasColumnName("plan_exercise_matrix_id");
        }

        #endregion
    }
}
