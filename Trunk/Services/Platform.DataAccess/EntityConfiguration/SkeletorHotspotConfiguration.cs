using System;

using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SkeletorHotspotConfiguration : EntityTypeConfiguration<SkeletonHotspot>
    {
        #region Construction

        public SkeletorHotspotConfiguration()
        {
            HasRequired(f => f.Side);
            HasRequired(f => f.Region);
            HasRequired(f => f.Orientation);
            Property(p => p.Id).IsRequired();
        }

        #endregion
    }
}
