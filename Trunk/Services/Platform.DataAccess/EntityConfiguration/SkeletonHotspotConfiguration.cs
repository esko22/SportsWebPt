using System;

using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SkeletonHotspotConfiguration : EntityTypeConfiguration<SkeletonHotspot>
    {
        #region Construction

        public SkeletonHotspotConfiguration()
        {
            ToTable("SkeletonHotspot");
            HasRequired(f => f.Side).WithMany().Map(c => c.MapKey("side_id"));
            HasRequired(f => f.Region).WithMany().Map(c => c.MapKey("region_id"));
            HasRequired(f => f.Orientation).WithMany().Map(c => c.MapKey("orientation_id"));
            Property(p => p.Id).IsRequired();
        }

        #endregion
    }
}
