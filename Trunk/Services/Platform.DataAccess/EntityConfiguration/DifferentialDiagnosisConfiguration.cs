using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class DifferentialDiagnosisConfiguration : EntityTypeConfiguration<DifferentialDiagnosis>
    {
        #region Construction

        public DifferentialDiagnosisConfiguration()
        {
            ToTable("DifferentialDiagnosis");
            Property(p => p.Id).IsRequired().HasColumnName("differential_diagnosis_id");
            Property(p => p.SubmittedBy).IsRequired().HasColumnName("submitted_by");
            Property(p => p.SubmittedFor).IsRequired().HasColumnName("submitted_for");
            Property(p => p.ReviewedOn).HasColumnName("reviewed_on");
            Property(p => p.SumbittedOn).HasColumnName("submitted_on");
        }

        #endregion
    }
}
