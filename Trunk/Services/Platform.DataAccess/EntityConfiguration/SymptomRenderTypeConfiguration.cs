using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomRenderTypeConfiguration : EntityTypeConfiguration<SymptomRenderType>
    {
        #region Construction

        public SymptomRenderTypeConfiguration()
        {
            ToTable("SymptomRenderType");
            Property(p => p.RenderType).IsRequired().HasColumnName("render_type").HasMaxLength(50);
            Property(p => p.DefaultTemplate).IsRequired().HasColumnName("default_template").HasMaxLength(100);
        }

        #endregion
    }
}
