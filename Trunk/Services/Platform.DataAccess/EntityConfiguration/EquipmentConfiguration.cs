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
            Property(p => p.ProductInformation).HasColumnName("product_information").HasColumnType("TEXT");
            Property(p => p.Id).IsRequired().HasColumnName("equipment_id");
            Property(p => p.Category).IsRequired().HasColumnName("function_category");
        }

        #endregion

    }
}
