using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomConfiguration : EntityTypeConfiguration<Symptom>
    {
        public SymptomConfiguration()
        {
            ToTable("Symptom");
            Property(p => p.Description).IsRequired().HasColumnName("description").HasMaxLength(500);
            Property(p => p.Name).HasColumnName("name").HasMaxLength(100);
            Property(p => p.RenderTypeId).IsRequired().HasColumnName("render_type_id");
            Property(p => p.Id).HasColumnName("symptom_id");
            Property(p => p.RenderOptions).HasColumnName("render_options").HasMaxLength(1000);

            HasRequired(p => p.RenderType).WithMany().HasForeignKey(f => f.RenderTypeId);
        }
    }
}
