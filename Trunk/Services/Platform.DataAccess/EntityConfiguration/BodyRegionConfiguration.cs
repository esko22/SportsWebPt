using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class BodyRegionConfiguration : EntityTypeConfiguration<BodyRegion>
    {
        #region Construction 

        public BodyRegionConfiguration()
        {
            ToTable("BodyRegion");
            Property(p => p.Id).IsRequired().HasColumnName("region_id");
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(50);

        }

        #endregion
    }
}
