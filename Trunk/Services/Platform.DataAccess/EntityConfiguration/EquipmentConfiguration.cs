using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class EquipmentConfiguration : EntityTypeConfiguration<Equipment>
    {

        #region Construction

        public EquipmentConfiguration()
        {
            ToTable("Equipment");
            Property(p => p.CommonName).IsRequired().HasColumnName("common_name").HasMaxLength(500);
            Property(p => p.PriceRange).HasColumnName("price_range").HasMaxLength(50);
            Property(p => p.ProductLink1).HasColumnName("product_link_1").HasMaxLength(200);
            Property(p => p.ProductLink2).HasColumnName("product_link_2").HasMaxLength(200);
            Property(p => p.ProductLink3).HasColumnName("product_link_3").HasMaxLength(200);
            Property(p => p.ProductLinkName1).HasColumnName("product_link_name_1").HasMaxLength(50);
            Property(p => p.ProductLinkName2).HasColumnName("product_link_name_2").HasMaxLength(50);
            Property(p => p.ProductLinkName3).HasColumnName("product_link_name_3").HasMaxLength(50);
            Property(p => p.Id).IsRequired().HasColumnName("equipment_id");
            Property(p => p.Category).IsRequired().HasColumnName("function_category");
        }

        #endregion

    }
}
