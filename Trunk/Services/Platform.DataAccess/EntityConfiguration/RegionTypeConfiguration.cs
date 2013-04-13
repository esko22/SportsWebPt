using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class RegionTypeConfiguration : EntityTypeConfiguration<BodyRegion>
    {
        #region Construction 

        public RegionTypeConfiguration()
        {
            Property(p => p.Id).IsRequired().HasColumnName("region_id");
            Property(p => p.Name).IsRequired().HasColumnName("region").HasMaxLength(50);

        }

        #endregion
    }
}
