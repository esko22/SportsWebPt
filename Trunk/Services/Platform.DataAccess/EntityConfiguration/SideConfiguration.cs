using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SideConfiguration : EntityTypeConfiguration<Side>
    {
         #region Construction

        public SideConfiguration()
        {
            ToTable("Side");
            Property(p => p.Value).IsRequired().HasColumnName("value").HasMaxLength(50);
            Property(p => p.Id).IsRequired().HasColumnName("side_id");
        }

        #endregion
    }
}
