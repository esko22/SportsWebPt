using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryConfiguration : EntityTypeConfiguration<Injury>
    {
        #region Construction

        public InjuryConfiguration()
        {
            ToTable("Injury");
            Property(p => p.CommonName).IsRequired().HasColumnName("common_name").HasMaxLength(50);
            Property(p => p.MedicalName).HasColumnName("medical_name").HasMaxLength(100);
            Property(p => p.Description).IsRequired().HasColumnName("description").HasMaxLength(2000);
            Property(p => p.OpeningStatement).IsRequired().HasColumnName("opening_statement").HasMaxLength(2000);
            Property(p => p.Id).IsRequired().HasColumnName("injury_id");
        }
        
        #endregion
    }
}
