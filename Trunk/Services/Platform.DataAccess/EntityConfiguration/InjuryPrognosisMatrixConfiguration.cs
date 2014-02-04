using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryPrognosisMatrixConfiguration : EntityTypeConfiguration<InjuryPrognosisMatrixItem>
    {
        #region Construction

        public InjuryPrognosisMatrixConfiguration()
        {
            ToTable("InjuryPrognosisMatrix");
            Property(p => p.Duration).IsRequired().HasColumnName("duration").HasMaxLength(250);
            Property(p => p.PrognosisId).IsRequired().HasColumnName("prognosis_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_prognosis_matrix_id");

        }

        #endregion
    }
}
