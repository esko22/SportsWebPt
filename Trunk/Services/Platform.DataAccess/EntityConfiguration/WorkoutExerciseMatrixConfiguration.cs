using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class WorkoutExerciseMatrixConfiguration : EntityTypeConfiguration<WorkoutExerciseMatrixItem>
    {
        #region Construction

        public WorkoutExerciseMatrixConfiguration()
        {
            ToTable("WorkoutExerciseMatrix");
            Property(p => p.WorkoutId).IsRequired().HasColumnName("workout_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            Property(p => p.Id).IsRequired().HasColumnName("workout_exercise_matrix_id");
        }

        #endregion
    }
}
