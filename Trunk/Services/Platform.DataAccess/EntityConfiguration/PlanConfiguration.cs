﻿using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanConfiguration : EntityTypeConfiguration<Plan>
    {

        #region Construction

        public PlanConfiguration()
        {
            ToTable("Plan");
            Property(p => p.Description).HasColumnName("description").IsRequired().HasColumnType("TEXT");
            Property(p => p.Duration).IsRequired().HasColumnName("duration");
            Property(p => p.StructuresInvolved).HasColumnName("structures_used").HasColumnType("TEXT");
            Property(p => p.Instructions).HasColumnName("instructions").HasColumnType("TEXT");
            Property(p => p.RoutineName).HasColumnName("routine_name").HasMaxLength(100);
            Property(p => p.AnimationTag).HasMaxLength(20).HasColumnName("animation_tag");
            Property(p => p.Id).IsRequired().HasColumnName("plan_id");

            HasMany(m => m.PlanCategoryMatrixItems).WithRequired(r => r.Plan).HasForeignKey(fk => fk.PlanId);
        }

        #endregion

    }

    public class PlanPublishDetailConfiguration : EntityTypeConfiguration<PlanPublishDetail>
    {
        #region Construction

        public PlanPublishDetailConfiguration()
        {
            ToTable("PlanPublishDetail");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("plan_id");
            Property(p => p.PageName).IsRequired().HasColumnName("page_name").HasMaxLength(50);
            Property(p => p.Tags).HasColumnName("tags").HasColumnType("TEXT").IsOptional();
            Property(p => p.Visible).HasColumnName("visible");

            HasRequired(r => r.Plan).WithOptional(o => o.PublishDetail);
        }

        #endregion
    }
}
