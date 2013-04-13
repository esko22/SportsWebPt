using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SideTypeConfiguration : EntityTypeConfiguration<SideType>
    {
         #region Construction

        public SideTypeConfiguration()
        {
            Property(p => p.Side).IsRequired().HasColumnName("side").HasMaxLength(50);
            Property(p => p.Id).IsRequired().HasColumnName("side_id");
        }

        #endregion
    }
}
