using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomDetailConfiguration : EntityTypeConfiguration<SymptomDetail>
    {
         #region Construction

        public SymptomDetailConfiguration()
        {
            ToTable("SymptomDetail");
            Property(p => p.DifferentialDiagnosisId).IsRequired().HasColumnName("differential_diagnosis_id");
            Property(p => p.SymptomMatrixItemId).HasColumnName("symptom_matrix_item_id");
            Property(p => p.GivenResponse).IsRequired().HasColumnName("given_response");
            HasKey(p => new {p.DifferentialDiagnosisId, p.SymptomMatrixItemId});
        }
        
        #endregion
    }
}
