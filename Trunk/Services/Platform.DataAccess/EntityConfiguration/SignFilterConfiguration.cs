using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SignFilterCategoryConfiguration: EntityTypeConfiguration<SignFilter>
    {
        #region Construction

        public SignFilterCategoryConfiguration()
        {
            ToTable("SignFilter");
            Property(p => p.FilterCategory).IsRequired().HasColumnName("filter_category").IsRequired().HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("sign_filter_id");

            HasMany(p => p.Signs).WithRequired(r => r.Filter).HasForeignKey(f => f.FilterCategoryId);
        }

        #endregion
    }
}
