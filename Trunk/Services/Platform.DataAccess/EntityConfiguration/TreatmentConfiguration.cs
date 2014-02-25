using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class TreatmentConfiguration: EntityTypeConfiguration<Treatment>
    {
        #region Construction

        public TreatmentConfiguration()
        {
            ToTable("Treatment");
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
            Property(p => p.Description).HasColumnName("description").HasColumnType("TEXT");
            Property(p => p.Category).HasColumnName("treatment_category_id");
            Property(p => p.Provider).HasColumnName("provider_type_id");
            Property(p => p.Id).IsRequired().HasColumnName("treatment_id");
        }
        
        #endregion
    }
}
