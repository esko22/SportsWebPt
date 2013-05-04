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
            Property(p => p.RenderType).IsRequired().HasColumnName("render_type");
            Property(p => p.Id).HasColumnName("symptom_id");
        }
    }
}
