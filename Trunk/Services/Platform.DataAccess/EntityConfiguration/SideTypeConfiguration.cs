using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class SideTypeConfiguration : EntityTypeConfiguration<Side>
    {
         #region Construction

        public SideTypeConfiguration()
        {
            Property(p => p.Value).IsRequired().HasColumnName("side").HasMaxLength(50);
            Property(p => p.Id).IsRequired().HasColumnName("side_id");
        }

        #endregion
    }
}
