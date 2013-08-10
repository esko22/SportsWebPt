using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SymptomMatrixConfiguration : EntityTypeConfiguration<SymptomMatrixItem>
    {
        #region Construction

        public SymptomMatrixConfiguration()
        {
            ToTable("SymptomMatrix");
            //Property(p => p.Decom).HasColumnName("decom");
            Property(p => p.BodyPartMatrixItemId).HasColumnName("body_part_matrix_id");
            Property(p => p.SymptomId).HasColumnName("symptom_id");
            Property(p => p.Id).HasColumnName("symptom_matrix_id");
        }

        #endregion
    }
}
