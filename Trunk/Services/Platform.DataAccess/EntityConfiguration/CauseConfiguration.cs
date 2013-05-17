using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class CauseConfiguration : EntityTypeConfiguration<Cause>
    {
        #region Construction

        public CauseConfiguration()
        {
            ToTable("Cause");
            Property(p => p.Description).IsRequired().HasColumnName("description").HasMaxLength(500);
            Property(p => p.Id).IsRequired().HasColumnName("cause_id");
        }
        
        #endregion
    }
}
