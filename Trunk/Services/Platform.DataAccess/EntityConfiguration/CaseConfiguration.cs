using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class CaseConfiguration : EntityTypeConfiguration<Case>
    {
        #region Construction

        public CaseConfiguration()
        {
            ToTable("Case");
            Property(p => p.Id).IsRequired().HasColumnName("case_id");
            Property(p => p.CreateDate).HasColumnName("create_date");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.PatientId).HasColumnName("patient_id");
            Property(p => p.ClincTherapistMatrixItemId).HasColumnName("clinic_therapist_id");
            Property(p => p.Name).HasColumnName("name").HasMaxLength(500);

            HasRequired(p => p.Patient).WithMany().HasForeignKey(f => f.PatientId);
            HasRequired(p => p.ClinicTherapistMatrixItem).WithMany().HasForeignKey(f => f.ClincTherapistMatrixItemId);
        }

        #endregion
    }
}
