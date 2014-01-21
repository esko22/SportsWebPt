using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryTreatmentMatrixConfiguration : EntityTypeConfiguration<InjuryTreatmentMatrixItem>
    {
        #region Construction

        public InjuryTreatmentMatrixConfiguration()
        {
            ToTable("InjuryTreatmentMatrix");
            Property(p => p.TreatmentId).IsRequired().HasColumnName("treatment_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_treatment_matrix_id");

        }

        #endregion
    }
}
