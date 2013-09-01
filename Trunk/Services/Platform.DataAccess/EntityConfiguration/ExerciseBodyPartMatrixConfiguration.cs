using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseBodyPartMatrixConfiguration : EntityTypeConfiguration<ExerciseBodyPartMatrixItem>
    {
        #region Construction

        public ExerciseBodyPartMatrixConfiguration()
        {
            ToTable("ExerciseBodyPartMatrix");
            Property(p => p.BodyPartId).IsRequired().HasColumnName("body_part_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            Property(p => p.Id).IsRequired().HasColumnName("exercise_body_part_matrix_id");
        }

        #endregion
    }
}
