using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PrognosisConfiguration: EntityTypeConfiguration<Prognosis>
    {
        #region Construction

        public PrognosisConfiguration()
        {
            ToTable("Prognosis");
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
            Property(p => p.Description).HasColumnName("description").HasColumnType("TEXT");
            Property(p => p.Category).HasColumnName("prognosis_category_id");
            Property(p => p.Id).IsRequired().HasColumnName("prognosis_id");
        }
        
        #endregion
    }
}
