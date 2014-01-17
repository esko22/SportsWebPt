using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class FilterConfiguration: EntityTypeConfiguration<Filter>
    {
        #region Construction

        public FilterConfiguration()
        {
            ToTable("Filter");
            Property(p => p.FilterCategory).IsRequired().HasColumnName("filter_category").IsRequired().HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("filter_id");
            Property(p => p.FilterType).IsRequired().HasColumnName("filter_type_id");

            HasMany(p => p.Signs).WithRequired(r => r.Filter).HasForeignKey(f => f.FilterId);
            HasMany(p => p.Causes).WithRequired(r => r.Filter).HasForeignKey(f => f.FilterId);
        }

        #endregion
    }
}
