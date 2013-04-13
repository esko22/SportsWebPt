using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PartTypeConfiguration : EntityTypeConfiguration<BodyPart>
    {
        #region Construction

        public PartTypeConfiguration()
        {
            Property(p => p.CommonName).IsRequired().HasColumnName("common_name").HasMaxLength(50);
            Property(p => p.ScientificName).HasColumnName("scientific_name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("part_id");
        }
        
        #endregion
    }
}
