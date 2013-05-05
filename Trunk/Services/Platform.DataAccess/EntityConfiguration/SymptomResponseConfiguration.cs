using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomResponseConfiguration : EntityTypeConfiguration<SymptomResponse>
    {
         #region Construction

        public SymptomResponseConfiguration()
        {
            ToTable("SymptomResponse");
            Property(p => p.DifferentialDiagnosisId).IsRequired().HasColumnName("differential_diagnosis_id");
            Property(p => p.SymptomMatrixItemId).HasColumnName("symptom_matrix_item_id");
            Property(p => p.GivenResponse).IsRequired().HasColumnName("given_response").HasMaxLength(2000);
            HasKey(p => new {p.DifferentialDiagnosisId, p.SymptomMatrixItemId});
        }
        
        #endregion
    }
}
