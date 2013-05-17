using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class WorkoutConfiguration : EntityTypeConfiguration<Workout>
    {

        #region Construction

        public WorkoutConfiguration()
        {
            ToTable("Workout");
            Property(p => p.Category).IsRequired().HasColumnName("category");
            Property(p => p.Description).HasColumnName("description").IsRequired().HasMaxLength(1000);
            Property(p => p.Duration).IsRequired().HasColumnName("duration");
            Property(p => p.MusclesInvolved).HasColumnName("muscles_used").HasMaxLength(500);
            Property(p => p.RoutineName).HasColumnName("routine_name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("workout_id");
        }

        #endregion

    }
}
