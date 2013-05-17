using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseVideoMatrixConfiguration : EntityTypeConfiguration<ExerciseVideoMatrixItem>
    {
        #region Construction

        public ExerciseVideoMatrixConfiguration()
        {
            ToTable("ExerciseVideoMatrix");
            Property(p => p.VideoId).IsRequired().HasColumnName("video_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            Property(p => p.Id).IsRequired().HasColumnName("exercise_video_matrix_id");
        }

        #endregion
    }
}
