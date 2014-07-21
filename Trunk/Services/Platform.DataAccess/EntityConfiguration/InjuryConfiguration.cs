using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryConfiguration : EntityTypeConfiguration<Injury>
    {
        #region Construction

        public InjuryConfiguration()
        {
            ToTable("Injury");
            Property(p => p.CommonName).IsRequired().HasColumnName("common_name").HasMaxLength(50);
            Property(p => p.MedicalName).HasColumnName("medical_name").HasMaxLength(100);
            Property(p => p.Description).IsRequired().HasColumnName("description").HasColumnType("TEXT");
            Property(p => p.Recovery).IsRequired().HasColumnName("recovery").HasColumnType("TEXT");
            Property(p => p.Id).IsRequired().HasColumnName("injury_id");
            Property(p => p.Severity).IsRequired().HasColumnName("severity");
            Property(p => p.AnimationTag).HasMaxLength(20).HasColumnName("animation_tag");
        }
        
        #endregion
    }


    public class InjuryPublishDetailConfiguration : EntityTypeConfiguration<InjuryPublishDetail>
    {
        #region Construction

        public InjuryPublishDetailConfiguration()
        {
            ToTable("InjuryPublishDetail");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("injury_id");
            Property(p => p.PageName).IsRequired().HasColumnName("page_name").HasMaxLength(50);
            Property(p => p.Tags).HasColumnName("tags").HasColumnType("TEXT").IsOptional();
            Property(p => p.OpeningStatement).IsRequired().HasColumnName("opening_statement").HasColumnType("TEXT");
            Property(p => p.Visible).HasColumnName("visible");

            HasRequired(r => r.Injury).WithOptional(o => o.PublishDetail);
        }

        #endregion
    }
}
