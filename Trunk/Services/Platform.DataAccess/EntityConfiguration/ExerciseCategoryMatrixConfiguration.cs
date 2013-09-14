using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseCategoryMatrixConfiguration : EntityTypeConfiguration<ExerciseCategoryMatrixItem>
    {
        #region Construction

        public ExerciseCategoryMatrixConfiguration()
        {
            ToTable("ExerciseCategoryMatrix");
            HasKey(p => new {p.ExerciseId, p.Category});
            Property(p => p.Category).IsRequired().HasColumnName("function_category_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
        }

        #endregion
    }
}
