using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseEquipmentMatrixConfiguration : EntityTypeConfiguration<ExerciseEquipmentMatrixItem>
    {

        #region Construction

        public ExerciseEquipmentMatrixConfiguration()
        {
            ToTable("ExerciseEquipmentMatrix");
            Property(p => p.EquipmentId).IsRequired().HasColumnName("equipment_id");
            Property(p => p.ExerciseId).IsRequired().HasColumnName("exercise_id");
            
            Property(p => p.Id).IsRequired().HasColumnName("exercise_equipment_matrix_id");
        }

        #endregion
    }
}
