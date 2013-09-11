using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class VendorConfiguration: EntityTypeConfiguration<Vendor>
    {
        #region Construction

        public VendorConfiguration()
        {
            ToTable("Vendor");
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("vendor_id");
            Property(p => p.Url).IsOptional().HasColumnName("url").HasMaxLength(300);
        }

        #endregion
    }
}
