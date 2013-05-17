using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryWorkoutMatrixConfiguration : EntityTypeConfiguration<InjuryWorkoutMatrixItem>
    {

        #region Construction

        public InjuryWorkoutMatrixConfiguration()
        {
            ToTable("InjuryWorkoutMatrix");
            Property(p => p.WorkoutId).IsRequired().HasColumnName("workout_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_workout_matrix_id");
        }

        #endregion

    }
}
