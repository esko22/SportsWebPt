using System;

using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SkeletonAreaConfiguration : EntityTypeConfiguration<SkeletonArea>
    {
        #region Construction

        public SkeletonAreaConfiguration()
        {
            ToTable("SkeletonArea");
            HasRequired(f => f.Side).WithMany().Map(c => c.MapKey("side_id"));
            HasRequired(f => f.Region).WithMany().Map(c => c.MapKey("region_id"));
            HasRequired(f => f.Orientation).WithMany().Map(c => c.MapKey("orientation_id"));

            Property(p => p.DisplayName).IsRequired().HasColumnName("display_name");
            Property(p => p.CssClassName).HasColumnName("css_class_name");
            Property(p => p.Id).IsRequired().HasColumnName("skeleton_area_id");
        }

        #endregion
    }
}
