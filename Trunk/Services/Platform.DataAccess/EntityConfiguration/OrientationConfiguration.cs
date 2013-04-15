using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class OrientationConfiguration : EntityTypeConfiguration<Orientation>
    {
        #region Construction

        public OrientationConfiguration()
        {
            ToTable("Orientation");
            Property(p => p.Id).IsRequired().HasColumnName("orientation_id");
            Property(p => p.Value).IsRequired().HasColumnName("value").HasMaxLength(20);
        }

        #endregion
    }
}
