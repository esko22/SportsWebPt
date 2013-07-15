using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseBodyRegionMatrixConfiguration : EntityTypeConfiguration<ExerciseBodyRegionMatrixItem>
    {
        #region Construction

        public ExerciseBodyRegionMatrixConfiguration()
        {
            ToTable("ExerciseBodyRegionMatrix");
            Property(p => p.BodyRegionId).IsRequired().HasColumnName("body_region_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            Property(p => p.Id).IsRequired().HasColumnName("exercise_body_region_matrix_id");
        }

        #endregion
    }
}
